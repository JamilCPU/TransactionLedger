using Backend.data.entities;
namespace Backend.repository.intrface {
    public interface IAccountRepository
    {
        Task<AccountEntity> CreateAccount(AccountEntity account);

        Task<AccountEntity> UpdateAccount(AccountEntity account);

        Task<List<AccountEntity>?> GetAllAccountsByUserId(int userId);
        Task DeleteAccount(int id);

        Task<AccountEntity?> GetAccountById(int id);
        Task<List<AccountEntity>?> GetAllAccounts();
    }
}