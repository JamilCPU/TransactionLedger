using System.Threading.Tasks;
using Backend.data;
using Backend.repository.intrface;
using Backend.data.entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.repository.impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;
        public async Task createAccount(AccountEntity account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task updateAccount(AccountEntity account)
        {

        }

        public async Task deleteAccount(int id)
        {

        }

        public async Task<AccountEntity?> getAccountById(int id)
        {
            return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<AccountEntity>?> getAllAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }
    }
}