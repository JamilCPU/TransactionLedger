using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.EntityFrameworkCore;
using Backend.data;
namespace Backend.repository.impl
{
    public class TransactionRepository : ITransactionRepository
    {
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

        private readonly BankContext _context;

        public TransactionRepository(BankContext context)
        {
            _context = context;
        }
    }
}