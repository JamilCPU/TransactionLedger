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
    }
}