namespace Backend.data.entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public AccountEntity Account { get; set; }
    }
}