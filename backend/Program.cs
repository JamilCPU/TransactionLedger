using Microsoft.EntityFrameworkCore;
using Backend.data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlite("Data Source=bank.db"));

var app = builder.Build();

app.Run();