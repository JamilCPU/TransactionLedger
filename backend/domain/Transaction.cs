using System;

public class Transaction
{
    public enum TransactionTypeEnum
    {
        DEPOSIT, WITHDRAWAL, TRANSFER
    }
    
    public int Id { get; }
    public int Amount { get; set; }
    public TransactionTypeEnum TransactionType { get; set; }
    public DateTime Timestamp { get; set; }
    public int AccountId { get; set; }

    public Transaction(int id, int amount, TransactionTypeEnum transactionType, int accountId)
    {
        this.Id = id;
        this.Amount = amount;
        this.TransactionType = transactionType;
        this.AccountId = accountId;
        this.Timestamp = DateTime.UtcNow;
    }

    public Transaction(int amount, TransactionTypeEnum transactionType, int accountId)
    {
        this.Amount = amount;
        this.TransactionType = transactionType;
        this.AccountId = accountId;
        this.Timestamp = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return $"Transaction {{ Id: {Id}, Amount: {Amount}, TransactionType: {TransactionType}, AccountId: {AccountId}, Timestamp: {Timestamp} }}";
    }
}