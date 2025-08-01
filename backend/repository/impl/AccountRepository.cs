using System.Threading.Tasks;
using Backend.data;
using Backend.repository.intrface;
using Backend.data.entities;

namespace Backend.repository.impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;
        public async void createAccount(AccountEntity account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public void updateAccount(AccountEntity account)
        {

        }

        public void deleteAccount(int id)
        {

        }

        public async AccountEntity getAccountById(int id)
        {
            return await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public List<AccountEntity> getAllAccounts()
        {

        }
    }
}