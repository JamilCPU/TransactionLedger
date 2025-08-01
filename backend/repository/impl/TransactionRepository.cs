using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.EntityFrameworkCore;
using Backend.data;
namespace Backend.repository.impl
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext _context;

        public async Task createTransaction(TransactionEntity transaction)
        {
        }

        public async Task updateTransaction(TransactionEntity transaction)
        {
        }

        public async Task deleteTransaction(int id)
        {
        }

        public async Task<TransactionEntity?> getTransactionById(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TransactionEntity>?> getAllTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}