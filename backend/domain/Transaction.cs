public class Transaction
{

    public enum TransactionTypeEnum
    {
        DEPOSIT, WITHDRAWAL, TRANSFER
    }
    private int Id { get; }
    private int Amount { get; set; }

    private TransactionTypeEnum TransactionType { get; set; }


    public Transaction(int id, int amount, TransactionTypeEnum transactionType)
    {
        this.Id = id;
        this.Amount = amount;
        this.TransactionType = transactionType;
    }
}