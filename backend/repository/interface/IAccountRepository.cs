using Backend.data.entities;
namespace Backend.repository.intrface {
    public interface IAccountRepository
    {
        void createAccount(AccountEntity account);
        void updateAccount(AccountEntity account);
        void deleteAccount(int id);
        AccountEntity getAccountById(int id);
        List<AccountEntity> getAllAccounts();
    }
}