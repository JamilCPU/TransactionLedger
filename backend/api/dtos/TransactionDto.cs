using Backend.data.entities;

namespace Backend.api.dtos
{
    public class TransactionDto
    {
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public TransactionEntity.TransactionTypeEnum TransactionType { get; set; }

        public TransactionDto(decimal amount, int accountId, TransactionEntity.TransactionTypeEnum transactionType)
        {
            this.Amount = amount;
            this.AccountId = accountId;
            this.TransactionType = transactionType;
        }
    }

    public class TransactionRequestDto
    {
        public decimal Amount { get; set; }

        public TransactionRequestDto(decimal amount)
        {
            this.Amount = amount;
        }
    }
}