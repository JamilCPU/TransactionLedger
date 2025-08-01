using Microsoft.EntityFrameworkCore;
using Backend.data.entities;
namespace Backend.data
{
    public class BankContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        public BankContext(DbContextOptions<BankContext> options) : base(options) { }
    }
}