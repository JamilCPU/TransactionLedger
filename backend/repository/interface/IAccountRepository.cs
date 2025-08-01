using Backend.data.entities;
namespace Backend.repository.intrface {
    public interface IAccountRepository
    {
        Task createAccount(AccountEntity account);
        Task updateAccount(AccountEntity account);
        Task deleteAccount(int id);
        Task<AccountEntity?> getAccountById(int id);
        Task<List<AccountEntity>?> getAllAccounts();
    }
}