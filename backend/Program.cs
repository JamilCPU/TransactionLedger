// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlite("Data Source=bank.db"));