public class Account {
        public enum AccountTypeEnum{
        CHECKING, SAVINGS
    }

    public enum AccountStatusEnum{
        ACTIVE, INACTIVE
    }
    private int Id {get;}
    private int Balance {get; set;}
    private int UserId {get;}
    private AccountTypeEnum AccountType {get; set;}
    private AccountStatusEnum AccountStatus {get; set;}

    public Account(int id, AccountTypeEnum accountType, int userId){
        this.Id = id;
        this.Balance = 0;
        this.AccountType = accountType;
        this.UserId = userId;
        this.AccountStatus = AccountStatusEnum.ACTIVE;
        
    }
}