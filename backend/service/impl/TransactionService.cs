using Backend.repository.intrface;
using Backend.api.dtos;
using Backend.data.entities;
using Backend.service.intrface;
using System.Threading.Tasks;

namespace Backend.service.impl
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository, ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }
        public async Task<TransactionEntity> CreateTransaction(TransactionDto transactionDto)
        {
            _logger.LogInformation(
                "CreateTransaction - AccountId: {AccountId}, Amount: {Amount}, TransactionType: {TransactionType}",
                transactionDto.AccountId,
                transactionDto.Amount,
                transactionDto.TransactionType
            );

            try
            {
                ValidateTransactionDto(transactionDto);

                // First get the account to satisfy the required Account property
                var account = await _accountRepository.GetAccountById(transactionDto.AccountId);
                if (account == null)
                {
                    _logger.LogWarning("CreateTransaction - Account not found - AccountId: {AccountId}", transactionDto.AccountId);
                    throw new ArgumentException("Account not found");
                }

                TransactionEntity transactionEntity = new TransactionEntity
                {
                    Amount = (int)transactionDto.Amount,
                    TransactionType = transactionDto.TransactionType,
                    AccountId = transactionDto.AccountId,
                    Account = account,
                    Timestamp = DateTime.UtcNow
                };

                var transaction = await _transactionRepository.createTransaction(transactionEntity);
                
                _logger.LogInformation(
                    "CreateTransaction - Success - TransactionId: {TransactionId}, AccountId: {AccountId}, Amount: {Amount}",
                    transaction.Id,
                    transaction.AccountId,
                    transaction.Amount
                );

                return transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "CreateTransaction - ERROR - AccountId: {AccountId}, Amount: {Amount}. Exception: {ExceptionType}",
                    transactionDto.AccountId,
                    transactionDto.Amount,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<TransactionEntity?> GetTransactionById(int id)
        {
            _logger.LogDebug("GetTransactionById - TransactionId: {TransactionId}", id);
            var transaction = await _transactionRepository.getTransactionById(id);
            
            if (transaction == null)
            {
                _logger.LogWarning("GetTransactionById - Transaction not found - TransactionId: {TransactionId}", id);
            }
            else
            {
                _logger.LogDebug("GetTransactionById - Found - TransactionId: {TransactionId}, AccountId: {AccountId}", id, transaction.AccountId);
            }
            
            return transaction;
        }

        public async Task<List<TransactionEntity>> GetAllTransactions()
        {
            _logger.LogDebug("GetAllTransactions - Request received");
            var transactions = await _transactionRepository.getAllTransactions();
            var count = transactions?.Count ?? 0;
            
            _logger.LogDebug("GetAllTransactions - Retrieved {Count} transactions", count);
            return transactions ?? new List<TransactionEntity>();
        }

        public async Task<List<TransactionEntity>> GetTransactionsByAccountId(int accountId)
        {
            _logger.LogDebug("GetTransactionsByAccountId - AccountId: {AccountId}", accountId);
            
            try
            {
                // This would need to be implemented in the repository layer
                // For now, we'll get all transactions and filter by account
                var allTransactions = await _transactionRepository.getAllTransactions();
                if (allTransactions == null)
                {
                    _logger.LogDebug("GetTransactionsByAccountId - No transactions found for AccountId: {AccountId}", accountId);
                    return new List<TransactionEntity>();
                }
                
                var filtered = allTransactions.Where(t => t.AccountId == accountId).ToList();
                _logger.LogDebug(
                    "GetTransactionsByAccountId - Found {Count} transactions for AccountId: {AccountId}",
                    filtered.Count,
                    accountId
                );
                
                return filtered;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "GetTransactionsByAccountId - ERROR - AccountId: {AccountId}. Exception: {ExceptionType}",
                    accountId,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task DeleteTransaction(int id)
        {
            _logger.LogInformation("DeleteTransaction - TransactionId: {TransactionId}", id);
            
            try
            {
                await _transactionRepository.deleteTransaction(id);
                _logger.LogInformation("DeleteTransaction - Success - TransactionId: {TransactionId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "DeleteTransaction - ERROR - TransactionId: {TransactionId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        private void ValidateTransactionDto(TransactionDto transactionDto)
        {
            _logger.LogDebug("ValidateTransactionDto - Validating transaction request");
            if (transactionDto.Amount <= 0)
            {
                _logger.LogWarning("ValidateTransactionDto - Validation failed: Amount must be greater than 0 - Amount: {Amount}", transactionDto.Amount);
                throw new ArgumentException("Transaction amount must be greater than 0");
            }
        }
    }
}