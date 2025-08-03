using Backend.repository.intrface;

namespace Backend.service.impl
{
    public class AccountService : IAccountService
    {
        public void deposit(int id, decimal amount)
        {
        }

        public void withdraw(int id, decimal amount)
        {
        }

        public void transfer(int fromAccountId, int toAccountId, decimal amount)
        {
        }

        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
    }
}