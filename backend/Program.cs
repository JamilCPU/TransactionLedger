using Microsoft.EntityFrameworkCore;
using Backend.data;
using Backend.service.intrface;
using Backend.service.impl;
using Backend.repository.intrface;
using Backend.repository.impl;
using Backend.api.options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using DotNetEnv;

namespace Backend
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            // Load .env file if it exists
            var envPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".env");
            if (File.Exists(envPath))
            {
                Env.Load(envPath);
            }
            else
            {
                // Try loading from backend directory
                var backendEnvPath = Path.Combine(AppContext.BaseDirectory, ".env");
                if (File.Exists(backendEnvPath))
                {
                    Env.Load(backendEnvPath);
                }
            }

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowFrontend",
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:5173")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });

            var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
            if (jwtOptions == null)
            {
                jwtOptions = new JwtOptions
                {
                    Key = "updatewithkeylater",
                    Issuer = "TransactionAPI",
                    Audience = "TransactionClient",
                    AccessTokenMinutes = 30
                };
            }

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddSingleton(jwtOptions);

            // Read EnableDetailedLogging from .env file (defaults to true if not set)
            var enableDetailedLogging = Env.GetString("EnableDetailedLogging", "true");
            var loggingOptions = new LoggingOptions 
            { 
                EnableDetailedLogging = bool.Parse(enableDetailedLogging) 
            };
            builder.Services.AddSingleton(loggingOptions);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();


            var dbPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "bank.db");
            builder.Services.AddDbContext<BankContext>(options =>
                options.UseSqlite($"Data Source={Path.GetFullPath(dbPath)}"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BankContext>();
                db.Database.Migrate();
            }

            app.UseCors("AllowFrontend");


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
