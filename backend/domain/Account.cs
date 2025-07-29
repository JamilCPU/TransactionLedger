public class Account {
        public enum AccountType{
        CHECKING, SAVINGS
    }

    public enum AccountStatus{
        ACTIVE, INACTIVE
    }
    private int id {get;}
    private int balance {get; set;}
    private int userId {get;}
    private AccountType accountType {get; set;}
    private AccountStatus accountStatus {get; set;}

    public Account(int id, AccountType accountType, int userId){
        this.id = id;
        this.balance = 0;
        this.accountType = accountType;
        this.userId = userId;
        this.accountStatus = AccountStatus.ACTIVE;
        
    }
}