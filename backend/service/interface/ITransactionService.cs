public interface ITransactionService{
    void createTransaction(int id, decimal amount);
    void getTransactionById(int id);
    void getAllTransactions(int id);
}