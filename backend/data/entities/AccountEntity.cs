namespace Backend.data.entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int Balance { get; set; }
        public int UserId { get; set; }
        public AccountType AccountType { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public UserEntity User { get; set; }
    }
}