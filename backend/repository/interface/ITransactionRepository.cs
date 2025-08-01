using Backend.data.entities;
namespace Backend.repository.intrface{
    public interface ITransactionRepository
    {
        void createTransaction(TransactionEntity transaction);
        void updateTransaction(TransactionEntity transaction);
        void deleteTransaction(int id);
        TransactionEntity getTransactionById(int id);
        List<Transaction> getAllTransactions();
    }
}