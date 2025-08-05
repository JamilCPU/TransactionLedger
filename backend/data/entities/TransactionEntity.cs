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
        public required AccountEntity Account { get; set; }

        public TransactionEntity()
        {
        }

        public TransactionEntity(int amount, TransactionTypeEnum transactionType)
        {
            this.Amount = amount;
            this.TransactionType = transactionType;
        }

        public override string ToString()
        {
            return $"TransactionEntity {{ Id: {Id}, Amount: {Amount}, TransactionType: {TransactionType} }}";
        }
    }
}