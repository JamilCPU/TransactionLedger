namespace Backend.data.entities
{

    public class AccountEntity
    {

        public enum AccountTypeEnum
        {
            CHECKING, SAVINGS
        }

        public enum AccountStatusEnum
        {
            ACTIVE, INACTIVE
        }
        public int Id { get; set; }
        public int Balance { get; set; }
        public int UserId { get; set; }
        public AccountTypeEnum AccountType { get; set; }
        public AccountStatusEnum AccountStatus { get; set; }
        public required UserEntity User { get; set; }

        public AccountEntity()
        {
        }

        public AccountEntity(int balance, int userId, AccountTypeEnum accountType, AccountStatusEnum accountStatus)
        {
            this.Balance = balance;
            this.UserId = userId;
            this.AccountType = accountType;
            this.AccountStatus = accountStatus;
        }

        public override string ToString()
        {
            return $"AccountEntity {{ Id: {Id}, Balance: {Balance}, UserId: {UserId}, AccountType: {AccountType}, AccountStatus: {AccountStatus} }}";
        }
    }
}