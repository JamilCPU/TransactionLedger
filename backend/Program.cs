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

namespace Backend
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
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
            
            // Add JWT Authentication
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
