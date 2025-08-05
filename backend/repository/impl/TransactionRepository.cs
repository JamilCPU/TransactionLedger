using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.EntityFrameworkCore;
using Backend.data;
namespace Backend.repository.impl
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<TransactionEntity> createTransaction(TransactionEntity transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            Console.WriteLine(transaction.ToString()); // debug line
            return transaction;
        }

        public async Task<TransactionEntity> updateTransaction(TransactionEntity transaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(transaction.Id);
            if (existingTransaction == null) return null;

            existingTransaction.Amount = transaction.Amount;
            existingTransaction.TransactionType = transaction.TransactionType;

            await _context.SaveChangesAsync();
            return existingTransaction;
        }

        public async Task deleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<TransactionEntity?> getTransactionById(int id)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TransactionEntity>?> getAllTransactions()
        {
            List<TransactionEntity> transactionEntities = [];
            transactionEntities = await _context.Transactions
                .Include(t => t.Account)
                .ToListAsync();
            return transactionEntities;
        }

        private readonly BankContext _context;

        public TransactionRepository(BankContext context)
        {
            _context = context;
        }
    }
}