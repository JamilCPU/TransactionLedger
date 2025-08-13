using Backend.repository.intrface;
using Backend.api.dtos;
using Backend.data.entities;
using Backend.service.intrface;
using System.Threading.Tasks;

namespace Backend.service.impl
{
    public class TransactionService : ITransactionService
    {
        public async Task<TransactionEntity> CreateTransaction(TransactionDto transactionDto)
        {
            Console.WriteLine("Service: Attempting to CREATE transaction for account: " + transactionDto.AccountId);
            ValidateTransactionDto(transactionDto);

            // First get the account to satisfy the required Account property
            var account = await _accountRepository.GetAccountById(transactionDto.AccountId);
            if (account == null)
            {
                throw new ArgumentException("Account not found");
            }

            TransactionEntity transactionEntity = new TransactionEntity
            {
                Amount = (int)transactionDto.Amount,
                TransactionType = transactionDto.TransactionType,
                AccountId = transactionDto.AccountId,
                Account = account,
                Timestamp = DateTime.UtcNow
            };

            return await _transactionRepository.createTransaction(transactionEntity);
        }

        public async Task<TransactionEntity?> GetTransactionById(int id)
        {
            Console.WriteLine("Service: Attempting to GET transaction: " + id);
            return await _transactionRepository.getTransactionById(id);
        }

        public async Task<List<TransactionEntity>> GetAllTransactions()
        {
            Console.WriteLine("Service: Attempting to GET all transactions");
            var transactions = await _transactionRepository.getAllTransactions();
            return transactions ?? new List<TransactionEntity>();
        }

        public async Task<List<TransactionEntity>> GetTransactionsByAccountId(int accountId)
        {
            Console.WriteLine("Service: Attempting to GET transactions for account: " + accountId);
            // This would need to be implemented in the repository layer
            // For now, we'll get all transactions and filter by account
            var allTransactions = await _transactionRepository.getAllTransactions();
            if (allTransactions == null) return new List<TransactionEntity>();
            
            return allTransactions.Where(t => t.AccountId == accountId).ToList();
        }

        public async Task DeleteTransaction(int id)
        {
            Console.WriteLine("Service: Attempting to DELETE transaction: " + id);
            await _transactionRepository.deleteTransaction(id);
        }

        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        private void ValidateTransactionDto(TransactionDto transactionDto)
        {
            Console.WriteLine("Perform TransactionDto validations");
            if (transactionDto.Amount <= 0)
            {
                throw new ArgumentException("Transaction amount must be greater than 0");
            }
        }

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
    }
}