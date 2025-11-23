using Backend.repository.intrface;
using Backend.api.dtos;
using Backend.data.entities;
using Backend.service.intrface;
using Backend.api.options;
using static Backend.api.options.LoggingExtensions;
using System.Threading.Tasks;

namespace Backend.service.impl
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly LoggingOptions _loggingOptions;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, ILogger<AccountService> logger, LoggingOptions loggingOptions)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _logger = logger;
            _loggingOptions = loggingOptions;
        }
        public async Task<AccountEntity> CreateAccount(AccountRequestDto accountRequestDto)
        {
            _logger.LogInformationIfEnabled(
                _loggingOptions,
                "CreateAccount - UserId: {UserId}, Amount: {Amount}, AccountType: {AccountType}",
                accountRequestDto.UserId,
                accountRequestDto.Amount,
                accountRequestDto.AccountType
            );

            try
            {
                ValidateAccountRequestDto(accountRequestDto);
                
                var user = await _userRepository.GetUserById(accountRequestDto.UserId);
                if (user == null)
                {
                    _logger.LogWarningIfEnabled(_loggingOptions, "CreateAccount - User not found - UserId: {UserId}", accountRequestDto.UserId);
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

                var account = await _accountRepository.CreateAccount(accountEntity);
                
                _logger.LogInformationIfEnabled(
                    _loggingOptions,
                    "CreateAccount - Success - AccountId: {AccountId}, AccountNumber: {AccountNumber}, UserId: {UserId}",
                    account.Id,
                    account.AccountNumber,
                    account.UserId
                );

                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "CreateAccount - ERROR - UserId: {UserId}, Amount: {Amount}. Exception: {ExceptionType}",
                    accountRequestDto.UserId,
                    accountRequestDto.Amount,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<AccountEntity> Deposit(int id, decimal amount)
        {
            _logger.LogInformationIfEnabled(
                _loggingOptions,
                "Deposit - AccountId: {AccountId}, Amount: {Amount}",
                id,
                amount
            );

            try
            {
                if (amount <= 0)
                {
                    _logger.LogWarningIfEnabled(_loggingOptions, "Deposit - Invalid amount - AccountId: {AccountId}, Amount: {Amount}", id, amount);
                    throw new ArgumentException("Deposit amount must be greater than 0");
                }

                var account = await _accountRepository.GetAccountById(id);
                if (account == null)
                {
                    _logger.LogWarningIfEnabled(_loggingOptions, "Deposit - Account not found - AccountId: {AccountId}", id);
                    throw new ArgumentException("Account not found");
                }

                var oldBalance = account.Balance;
                account.Balance += (int) amount;
                var updatedAccount = await _accountRepository.UpdateAccount(account);
                
                _logger.LogInformationIfEnabled(
                    _loggingOptions,
                    "Deposit - Success - AccountId: {AccountId}, OldBalance: {OldBalance}, NewBalance: {NewBalance}, Amount: {Amount}",
                    id,
                    oldBalance,
                    updatedAccount.Balance,
                    amount
                );

                return updatedAccount;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Deposit - ERROR - AccountId: {AccountId}, Amount: {Amount}. Exception: {ExceptionType}",
                    id,
                    amount,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<AccountEntity> Withdraw(int id, decimal amount)
        {
            _logger.LogInformationIfEnabled(
                _loggingOptions,
                "Withdraw - AccountId: {AccountId}, Amount: {Amount}",
                id,
                amount
            );

            try
            {
                if (amount <= 0)
                {
                    _logger.LogWarningIfEnabled(_loggingOptions, "Withdraw - Invalid amount - AccountId: {AccountId}, Amount: {Amount}", id, amount);
                    throw new ArgumentException("Withdrawal amount must be greater than 0");
                }

                var account = await _accountRepository.GetAccountById(id);
                if (account == null)
                {
                    _logger.LogWarningIfEnabled(_loggingOptions, "Withdraw - Account not found - AccountId: {AccountId}", id);
                    throw new ArgumentException("Account not found");
                }

                if (account.Balance < amount)
                {
                    _logger.LogWarningIfEnabled(
                        _loggingOptions,
                        "Withdraw - Insufficient funds - AccountId: {AccountId}, Balance: {Balance}, RequestedAmount: {Amount}",
                        id,
                        account.Balance,
                        amount
                    );
                    throw new InvalidOperationException("Insufficient funds");
                }

                var oldBalance = account.Balance;
                account.Balance -= (int) amount;
                var updatedAccount = await _accountRepository.UpdateAccount(account);
                
                _logger.LogInformationIfEnabled(
                    _loggingOptions,
                    "Withdraw - Success - AccountId: {AccountId}, OldBalance: {OldBalance}, NewBalance: {NewBalance}, Amount: {Amount}",
                    id,
                    oldBalance,
                    updatedAccount.Balance,
                    amount
                );

                return updatedAccount;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Withdraw - ERROR - AccountId: {AccountId}, Amount: {Amount}. Exception: {ExceptionType}",
                    id,
                    amount,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            _logger.LogInformationIfEnabled(
                _loggingOptions,
                "Transfer - FromAccountId: {FromAccountId}, ToAccountId: {ToAccountId}, Amount: {Amount}",
                fromAccountId,
                toAccountId,
                amount
            );

            try
            {
                if (amount <= 0)
                {
                    _logger.LogWarningIfEnabled(
                        _loggingOptions,
                        "Transfer - Invalid amount - FromAccountId: {FromAccountId}, ToAccountId: {ToAccountId}, Amount: {Amount}",
                        fromAccountId,
                        toAccountId,
                        amount
                    );
                    throw new ArgumentException("Transfer amount must be greater than 0");
                }

                var fromAccount = await _accountRepository.GetAccountById(fromAccountId);
                var toAccount = await _accountRepository.GetAccountById(toAccountId);

                if (fromAccount == null || toAccount == null)
                {
                    _logger.LogWarningIfEnabled(
                        _loggingOptions,
                        "Transfer - Account not found - FromAccountId: {FromAccountId} (Found: {FromFound}), ToAccountId: {ToAccountId} (Found: {ToFound})",
                        fromAccountId,
                        fromAccount != null,
                        toAccountId,
                        toAccount != null
                    );
                    throw new ArgumentException("One or both accounts not found");
                }

                if (fromAccount.Balance < amount)
                {
                    _logger.LogWarningIfEnabled(
                        _loggingOptions,
                        "Transfer - Insufficient funds - FromAccountId: {FromAccountId}, Balance: {Balance}, RequestedAmount: {Amount}",
                        fromAccountId,
                        fromAccount.Balance,
                        amount
                    );
                    throw new InvalidOperationException("Insufficient funds for transfer");
                }

                var fromOldBalance = fromAccount.Balance;
                var toOldBalance = toAccount.Balance;

                fromAccount.Balance -= (int) amount;
                toAccount.Balance += (int) amount;

                await _accountRepository.UpdateAccount(fromAccount);
                await _accountRepository.UpdateAccount(toAccount);
                
                _logger.LogInformationIfEnabled(
                    _loggingOptions,
                    "Transfer - Success - FromAccountId: {FromAccountId} (Balance: {FromOldBalance} -> {FromNewBalance}), ToAccountId: {ToAccountId} (Balance: {ToOldBalance} -> {ToNewBalance}), Amount: {Amount}",
                    fromAccountId,
                    fromOldBalance,
                    fromAccount.Balance,
                    toAccountId,
                    toOldBalance,
                    toAccount.Balance,
                    amount
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Transfer - ERROR - FromAccountId: {FromAccountId}, ToAccountId: {ToAccountId}, Amount: {Amount}. Exception: {ExceptionType}",
                    fromAccountId,
                    toAccountId,
                    amount,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<AccountEntity?> GetAccountById(int id)
        {
            _logger.LogDebugIfEnabled(_loggingOptions, "GetAccountById - AccountId: {AccountId}", id);
            var account = await _accountRepository.GetAccountById(id);
            
            if (account == null)
            {
                _logger.LogWarningIfEnabled(_loggingOptions, "GetAccountById - Account not found - AccountId: {AccountId}", id);
            }
            else
            {
                _logger.LogDebugIfEnabled(_loggingOptions, "GetAccountById - Found - AccountId: {AccountId}, AccountNumber: {AccountNumber}", id, account.AccountNumber);
            }
            
            return account;
        }

        public async Task<List<AccountEntity>> GetAllAccounts()
        {
            _logger.LogDebugIfEnabled(_loggingOptions, "GetAllAccounts - Request received");
            var accounts = await _accountRepository.GetAllAccounts();
            var count = accounts?.Count ?? 0;
            
            _logger.LogDebugIfEnabled(_loggingOptions, "GetAllAccounts - Retrieved {Count} accounts", count);
            return accounts ?? new List<AccountEntity>();
        }

        public async Task DeleteAccount(int id)
        {
            _logger.LogInformationIfEnabled(_loggingOptions, "DeleteAccount - AccountId: {AccountId}", id);
            
            try
            {
                await _accountRepository.DeleteAccount(id);
                _logger.LogInformationIfEnabled(_loggingOptions, "DeleteAccount - Success - AccountId: {AccountId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "DeleteAccount - ERROR - AccountId: {AccountId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        private void ValidateAccountRequestDto(AccountRequestDto accountRequestDto)
        {
            _logger.LogDebugIfEnabled(_loggingOptions, "ValidateAccountRequestDto - Validating account request");
            if (accountRequestDto.Amount < 0)
            {
                _logger.LogWarningIfEnabled(_loggingOptions, "ValidateAccountRequestDto - Validation failed: Amount is negative - Amount: {Amount}", accountRequestDto.Amount);
                throw new ArgumentException("Initial amount cannot be negative");
            }
        }
    }
}