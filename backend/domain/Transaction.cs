public class Transaction {
    private int id {get;}
    private int amount {get; set;}
    private enum TransactionType{
        DEPOSIT, WITHDRAWAL, TRANSFER
    }
}