using System;

namespace Backend.data.entities
{
    public class TransactionEntity
    {
        public enum TransactionTypeEnum
        {
            DEPOSIT, WITHDRAWAL, TRANSFER
        }
        public int Id { get; set; }
        public int Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime Timestamp { get; set; }
        public int AccountId { get; set; }
        public required AccountEntity Account { get; set; }

        public TransactionEntity()
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public TransactionEntity(int amount, TransactionTypeEnum transactionType)
        {
            this.Amount = amount;
            this.TransactionType = transactionType;
            this.Timestamp = DateTime.UtcNow;
        }

        public TransactionEntity(int amount, TransactionTypeEnum transactionType, int accountId)
        {
            this.Amount = amount;
            this.TransactionType = transactionType;
            this.AccountId = accountId;
            this.Timestamp = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"TransactionEntity {{ Id: {Id}, Amount: {Amount}, TransactionType: {TransactionType}, AccountId: {AccountId}, Timestamp: {Timestamp} }}";
        }
    }
}