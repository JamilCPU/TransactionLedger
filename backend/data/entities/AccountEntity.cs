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
        public UserEntity User { get; set; }
    }
}