public interface IAccountRepository{
    void createAccount(Account account);
    void updateAccount(Account account);
    void deleteAccount(int id);
    Account getAccountById(int id);
    List<Account> getAllAccounts();
}