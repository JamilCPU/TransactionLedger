namespace Backend.repository.intrface{
    public interface ITransactionRepository
    {
        void createTransaction(Transaction transaction);
        void updateTransaction(Transaction transaction);
        void deleteTransaction(int id);
        Transaction getTransactionById(int id);
        List<Transaction> getAllTransactions();
    }
}