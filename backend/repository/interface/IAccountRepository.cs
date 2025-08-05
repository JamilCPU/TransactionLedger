using Backend.data.entities;
namespace Backend.repository.intrface {
    public interface IAccountRepository
    {
        Task<AccountEntity> CreateAccount(AccountEntity account);
        Task<AccountEntity> updateAccount(AccountEntity account);
        Task deleteAccount(int id);
        Task<AccountEntity?> getAccountById(int id);
        Task<List<AccountEntity>?> getAllAccounts();
    }
}