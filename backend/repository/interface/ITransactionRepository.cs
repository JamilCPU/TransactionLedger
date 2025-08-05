using Backend.data.entities;
namespace Backend.repository.intrface{
    public interface ITransactionRepository
    {
        Task<TransactionEntity> createTransaction(TransactionEntity transaction);
        Task<TransactionEntity> updateTransaction(TransactionEntity transaction);
        Task deleteTransaction(int id);
        Task<TransactionEntity?> getTransactionById(int id);
        Task<List<TransactionEntity>?> getAllTransactions();
    }
}