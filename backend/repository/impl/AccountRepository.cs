using System.Threading.Tasks;
using Backend.data;
using Backend.repository.intrface;
using Backend.data.entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.repository.impl
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<AccountEntity> CreateAccount(AccountEntity account)
        {
            // Generate a unique 16-digit account number
            account.AccountNumber = await GenerateUniqueAccountNumber();
            
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<AccountEntity> UpdateAccount(AccountEntity account)
        {
            var existingAccount = await _context.Accounts.FindAsync(account.Id);
            if (existingAccount == null) return null;

            existingAccount.Balance = account.Balance;
            existingAccount.AccountType = account.AccountType;
            existingAccount.AccountStatus = account.AccountStatus;

            await _context.SaveChangesAsync();
            return existingAccount;
        }

        public async Task DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<AccountEntity?> GetAccountById(int id)
        {
            return await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<AccountEntity>?> GetAllAccountsByUserId(int userId)
        {
            List<AccountEntity> accountEntities = [];
            accountEntities = await _context.Accounts
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .ToListAsync();
            return accountEntities;
        }

        public async Task<List<AccountEntity>?> GetAllAccounts()
        {
            List<AccountEntity> accountEntities = [];
            accountEntities = await _context.Accounts
                .Include(a => a.User)
                .ToListAsync();
            return accountEntities;
        }
        
        private readonly BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        private async Task<string> GenerateUniqueAccountNumber()
        {
            string accountNumber;
            bool isUnique;
            
            do
            {
                // Generate a 16-digit account number
                accountNumber = GenerateRandomAccountNumber();
                
                // Check if this account number already exists
                isUnique = !await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
            }
            while (!isUnique);
            
            return accountNumber;
        }

        private string GenerateRandomAccountNumber()
        {
            var random = new Random();
            var accountNumber = new System.Text.StringBuilder(16);
            
            for (int i = 0; i < 16; i++)
            {
                accountNumber.Append(random.Next(0, 10));
            }
            
            return accountNumber.ToString();
        }
    }
}