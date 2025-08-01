using System.Threading.Tasks;

namespace Backend.repository.impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;
        public async Task createAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public void updateAccount(Account account)
        {

        }

        public void deleteAccount(int id)
        {

        }

        public async Account getAccountById(int id)
        {
            return await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public List<Account> getAllAccounts()
        {

        }
    }
}