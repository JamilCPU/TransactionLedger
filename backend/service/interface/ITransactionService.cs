using Backend.api.dtos;
using Backend.data.entities;

namespace Backend.service.intrface
{
    public interface ITransactionService
    {
        Task<TransactionEntity> CreateTransaction(TransactionDto transactionDto);
        Task<TransactionEntity?> GetTransactionById(int id);
        Task<List<TransactionEntity>> GetAllTransactions();
        Task<List<TransactionEntity>> GetTransactionsByAccountId(int accountId);
        Task DeleteTransaction(int id);
    }
}