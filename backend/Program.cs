using Microsoft.EntityFrameworkCore;
using Backend.data;

namespace Backend
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BankContext>(options =>
                options.UseSqlite("Data Source=bank.db"));

            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
