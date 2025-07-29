public interface IAccountService{
    void deposit(int id, decimal amount);
    void withdraw(int id, decimal amount);
    void transfer(int fromAccountId, int toAccountId, decimal amount);
}