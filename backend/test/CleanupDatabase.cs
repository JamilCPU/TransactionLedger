using Microsoft.EntityFrameworkCore;
using Backend.data;
using Backend.data.entities;

namespace Backend.api.test
{
    public static class CleanupDatabase
    {
        public static async Task CleanupAllTestData()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankContext>();
            var dbPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "bank.db");
            optionsBuilder.UseSqlite($"Data Source={Path.GetFullPath(dbPath)}");
            
            using var context = new BankContext(optionsBuilder.Options);
            
            try
            {
                // Delete in reverse order to respect foreign key constraints
                var transactions = await context.Transactions.ToListAsync();
                if (transactions.Any())
                {
                    context.Transactions.RemoveRange(transactions);
                    Console.WriteLine($"Deleted {transactions.Count} transactions");
                }
                
                var accounts = await context.Accounts.ToListAsync();
                if (accounts.Any())
                {
                    context.Accounts.RemoveRange(accounts);
                    Console.WriteLine($"Deleted {accounts.Count} accounts");
                }
                
                var users = await context.Users.ToListAsync();
                if (users.Any())
                {
                    context.Users.RemoveRange(users);
                    Console.WriteLine($"Deleted {users.Count} users");
                }
                
                await context.SaveChangesAsync();
                Console.WriteLine("Database cleanup completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during cleanup: {ex.Message}");
            }
        }
    }
} 