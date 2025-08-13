using Backend.repository.intrface;
using Backend.api.dtos;
using Backend.data.entities;
using Backend.service.intrface;
using System.Threading.Tasks;

namespace Backend.service.impl
{
    public class AccountService : IAccountService
    {
        public async Task<AccountEntity> CreateAccount(AccountRequestDto accountRequestDto)
        {
            Console.WriteLine("Service: Attempting to CREATE account for user: " + accountRequestDto.UserId);
            ValidateAccountRequestDto(accountRequestDto);
            
            // First get the user to satisfy the required User property
            var user = await _userRepository.GetUserById(accountRequestDto.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            
            AccountEntity accountEntity = new AccountEntity
            {
                Balance = (int)accountRequestDto.Amount,
                UserId = accountRequestDto.UserId,
                AccountType = accountRequestDto.AccountType,
                AccountStatus = AccountEntity.AccountStatusEnum.ACTIVE,
                User = user
            };

            return await _accountRepository.CreateAccount(accountEntity);
        }

        public async Task<AccountEntity> Deposit(int id, decimal amount)
        {
            Console.WriteLine("Service: Attempting to DEPOSIT to account: " + id);
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than 0");
            }

            var account = await _accountRepository.GetAccountById(id);
            if (account == null)
            {
                throw new ArgumentException("Account not found");
            }

            account.Balance += (int) amount;
            return await _accountRepository.UpdateAccount(account);
        }

        public async Task<AccountEntity> Withdraw(int id, decimal amount)
        {
            Console.WriteLine("Service: Attempting to WITHDRAW from account: " + id);
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be greater than 0");
            }

            var account = await _accountRepository.GetAccountById(id);
            if (account == null)
            {
                throw new ArgumentException("Account not found");
            }

            if (account.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            account.Balance -= (int) amount;
            return await _accountRepository.UpdateAccount(account);
        }

        public async Task Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            Console.WriteLine("Service: Attempting to TRANSFER from account: " + fromAccountId + " to account: " + toAccountId);
            if (amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be greater than 0");
            }

            var fromAccount = await _accountRepository.GetAccountById(fromAccountId);
            var toAccount = await _accountRepository.GetAccountById(toAccountId);

            if (fromAccount == null || toAccount == null)
            {
                throw new ArgumentException("One or both accounts not found");
            }

            if (fromAccount.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds for transfer");
            }

            fromAccount.Balance -= (int) amount;
            toAccount.Balance += (int) amount;

            await _accountRepository.UpdateAccount(fromAccount);
            await _accountRepository.UpdateAccount(toAccount);
        }

        public async Task<AccountEntity?> GetAccountById(int id)
        {
            Console.WriteLine("Service: Attempting to GET account: " + id);
            return await _accountRepository.GetAccountById(id);
        }

        public async Task<List<AccountEntity>> GetAllAccounts()
        {
            Console.WriteLine("Service: Attempting to GET all accounts");
            var accounts = await _accountRepository.GetAllAccounts();
            return accounts ?? new List<AccountEntity>();
        }

        public async Task DeleteAccount(int id)
        {
            Console.WriteLine("Service: Attempting to DELETE account: " + id);
            await _accountRepository.DeleteAccount(id);
        }

        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        private void ValidateAccountRequestDto(AccountRequestDto accountRequestDto)
        {
            Console.WriteLine("Perform AccountRequestDto validations");
            if (accountRequestDto.Amount < 0)
            {
                throw new ArgumentException("Initial amount cannot be negative");
            }
        }

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }
    }
}