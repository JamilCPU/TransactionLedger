using Backend.data.entities;
namespace Backend.repository.intrface{
    public interface ITransactionRepository
    {
        Task createTransaction(TransactionEntity transaction);
        Task updateTransaction(TransactionEntity transaction);
        Task deleteTransaction(int id);
        Task<TransactionEntity?> getTransactionById(int id);
        Task<List<TransactionEntity>?> getAllTransactions();
    }
}