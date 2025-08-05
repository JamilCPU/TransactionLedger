using Backend.api.dtos;
using Backend.data.entities;

namespace Backend.service.intrface
{
    public interface IAccountService
    {
        Task<AccountEntity> CreateAccount(AccountRequestDto accountRequestDto);
        Task<AccountEntity> Deposit(int id, decimal amount);
        Task<AccountEntity> Withdraw(int id, decimal amount);
        Task Transfer(int fromAccountId, int toAccountId, decimal amount);
        Task<AccountEntity?> GetAccountById(int id);
        Task<List<AccountEntity>> GetAllAccounts();
        Task DeleteAccount(int id);
    }
}