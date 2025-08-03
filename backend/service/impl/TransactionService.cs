using Backend.repository.intrface;

namespace Backend.service.impl
{
    public class TransactionService : ITransactionService
    {
        public void createTransaction(int id, decimal amount)
        {
        }

        public void getTransactionById(int id)
        {
        }

        public void getAllTransactions(int id)
        {
        }

        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
    }
}