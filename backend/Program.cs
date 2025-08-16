using Microsoft.EntityFrameworkCore;
using Backend.data;
using Backend.service.intrface;
using Backend.service.impl;
using Backend.repository.intrface;
using Backend.repository.impl;

namespace Backend
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

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

            app.MapControllers();

            app.Run();
        }
    }
}
