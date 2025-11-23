using System.Threading.Tasks;
using Backend.data;
using Backend.repository.intrface;
using Backend.data.entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.repository.impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(BankContext context, ILogger<AccountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<AccountEntity> CreateAccount(AccountEntity account)
        {
            _logger.LogDebug(
                "CreateAccount - UserId: {UserId}, AccountType: {AccountType}, InitialBalance: {Balance}",
                account.UserId,
                account.AccountType,
                account.Balance
            );

            try
            {
                // Generate a unique 16-digit account number
                account.AccountNumber = await GenerateUniqueAccountNumber();
                
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation(
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
                    "CreateAccount - Database ERROR - UserId: {UserId}. Exception: {ExceptionType}",
                    account.UserId,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<AccountEntity> UpdateAccount(AccountEntity account)
        {
            _logger.LogDebug(
                "UpdateAccount - AccountId: {AccountId}, NewBalance: {Balance}",
                account.Id,
                account.Balance
            );

            try
            {
                var existingAccount = await _context.Accounts.FindAsync(account.Id);
                if (existingAccount == null)
                {
                    _logger.LogWarning("UpdateAccount - Account not found - AccountId: {AccountId}", account.Id);
                    return null;
                }

                var oldBalance = existingAccount.Balance;
                existingAccount.Balance = account.Balance;
                existingAccount.AccountType = account.AccountType;
                existingAccount.AccountStatus = account.AccountStatus;

                await _context.SaveChangesAsync();
                
                _logger.LogDebug(
                    "UpdateAccount - Success - AccountId: {AccountId}, Balance: {OldBalance} -> {NewBalance}",
                    account.Id,
                    oldBalance,
                    existingAccount.Balance
                );

                return existingAccount;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "UpdateAccount - Database ERROR - AccountId: {AccountId}. Exception: {ExceptionType}",
                    account.Id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task DeleteAccount(int id)
        {
            _logger.LogDebug("DeleteAccount - AccountId: {AccountId}", id);

            try
            {
                var account = await _context.Accounts.FindAsync(id);
                if (account == null)
                {
                    _logger.LogWarning("DeleteAccount - Account not found - AccountId: {AccountId}", id);
                    return;
                }

                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("DeleteAccount - Success - AccountId: {AccountId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "DeleteAccount - Database ERROR - AccountId: {AccountId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<AccountEntity?> GetAccountById(int id)
        {
            _logger.LogTrace("GetAccountById - Querying database - AccountId: {AccountId}", id);
            
            try
            {
                var account = await _context.Accounts
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(a => a.Id == id);
                
                if (account != null)
                {
                    _logger.LogTrace("GetAccountById - Found - AccountId: {AccountId}", id);
                }
                
                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "GetAccountById - Database ERROR - AccountId: {AccountId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<List<AccountEntity>?> GetAllAccountsByUserId(int userId)
        {
            _logger.LogTrace("GetAllAccountsByUserId - Querying database - UserId: {UserId}", userId);
            
            try
            {
                List<AccountEntity> accountEntities = [];
                accountEntities = await _context.Accounts
                    .Include(a => a.User)
                    .Where(a => a.UserId == userId)
                    .ToListAsync();
                
                _logger.LogTrace("GetAllAccountsByUserId - Found {Count} accounts - UserId: {UserId}", accountEntities.Count, userId);
                return accountEntities;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "GetAllAccountsByUserId - Database ERROR - UserId: {UserId}. Exception: {ExceptionType}",
                    userId,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<List<AccountEntity>?> GetAllAccounts()
        {
            _logger.LogTrace("GetAllAccounts - Querying database");
            
            try
            {
                List<AccountEntity> accountEntities = [];
                accountEntities = await _context.Accounts
                    .Include(a => a.User)
                    .ToListAsync();
                
                _logger.LogTrace("GetAllAccounts - Found {Count} accounts", accountEntities.Count);
                return accountEntities;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "GetAllAccounts - Database ERROR. Exception: {ExceptionType}",
                    ex.GetType().Name
                );
                throw;
            }
        }
        
        private readonly BankContext _context;

        private async Task<string> GenerateUniqueAccountNumber()
        {
            _logger.LogTrace("GenerateUniqueAccountNumber - Generating unique account number");
            
            string accountNumber;
            bool isUnique;
            int attempts = 0;
            
            do
            {
                attempts++;
                // Generate a 16-digit account number
                accountNumber = GenerateRandomAccountNumber();
                
                // Check if this account number already exists
                isUnique = !await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
            }
            while (!isUnique);
            
            if (attempts > 1)
            {
                _logger.LogDebug("GenerateUniqueAccountNumber - Generated after {Attempts} attempts - AccountNumber: {AccountNumber}", attempts, accountNumber);
            }
            
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