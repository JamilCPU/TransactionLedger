using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.EntityFrameworkCore;
using Backend.data;
namespace Backend.repository.impl
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(BankContext context, ILogger<TransactionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TransactionEntity> createTransaction(TransactionEntity transaction)
        {
            _logger.LogDebug(
                "createTransaction - AccountId: {AccountId}, Amount: {Amount}, TransactionType: {TransactionType}",
                transaction.AccountId,
                transaction.Amount,
                transaction.TransactionType
            );

            try
            {
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation(
                    "createTransaction - Success - TransactionId: {TransactionId}, AccountId: {AccountId}, Amount: {Amount}",
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
                    "createTransaction - Database ERROR - AccountId: {AccountId}, Amount: {Amount}. Exception: {ExceptionType}",
                    transaction.AccountId,
                    transaction.Amount,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<TransactionEntity> updateTransaction(TransactionEntity transaction)
        {
            _logger.LogDebug("updateTransaction - TransactionId: {TransactionId}", transaction.Id);

            try
            {
                var existingTransaction = await _context.Transactions.FindAsync(transaction.Id);
                if (existingTransaction == null)
                {
                    _logger.LogWarning("updateTransaction - Transaction not found - TransactionId: {TransactionId}", transaction.Id);
                    return null;
                }

                existingTransaction.Amount = transaction.Amount;
                existingTransaction.TransactionType = transaction.TransactionType;

                await _context.SaveChangesAsync();
                
                _logger.LogDebug("updateTransaction - Success - TransactionId: {TransactionId}", transaction.Id);
                return existingTransaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "updateTransaction - Database ERROR - TransactionId: {TransactionId}. Exception: {ExceptionType}",
                    transaction.Id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task deleteTransaction(int id)
        {
            _logger.LogDebug("deleteTransaction - TransactionId: {TransactionId}", id);

            try
            {
                var transaction = await _context.Transactions.FindAsync(id);
                if (transaction == null)
                {
                    _logger.LogWarning("deleteTransaction - Transaction not found - TransactionId: {TransactionId}", id);
                    return;
                }

                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("deleteTransaction - Success - TransactionId: {TransactionId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "deleteTransaction - Database ERROR - TransactionId: {TransactionId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<TransactionEntity?> getTransactionById(int id)
        {
            _logger.LogTrace("getTransactionById - Querying database - TransactionId: {TransactionId}", id);
            
            try
            {
                var transaction = await _context.Transactions
                    .Include(t => t.Account)
                    .FirstOrDefaultAsync(t => t.Id == id);
                
                if (transaction != null)
                {
                    _logger.LogTrace("getTransactionById - Found - TransactionId: {TransactionId}", id);
                }
                
                return transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "getTransactionById - Database ERROR - TransactionId: {TransactionId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<List<TransactionEntity>?> getAllTransactions()
        {
            _logger.LogTrace("getAllTransactions - Querying database");
            
            try
            {
                List<TransactionEntity> transactionEntities = [];
                transactionEntities = await _context.Transactions
                    .Include(t => t.Account)
                    .ToListAsync();
                
                _logger.LogTrace("getAllTransactions - Found {Count} transactions", transactionEntities.Count);
                return transactionEntities;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "getAllTransactions - Database ERROR. Exception: {ExceptionType}",
                    ex.GetType().Name
                );
                throw;
            }
        }

        private readonly BankContext _context;
    }
}