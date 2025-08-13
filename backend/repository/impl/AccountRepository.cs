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
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            Console.WriteLine(account.ToString()); // debug line
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
    }
}