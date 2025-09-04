namespace Backend.data.entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<AccountEntity> Accounts { get; set; }

        public UserEntity(string username, string password, string email, string phone)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Phone = phone;
            this.Accounts = new List<AccountEntity>();
        }

        public UserEntity(string username, string password, string email, string phone, List<AccountEntity> accounts)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Phone = phone;
            this.Accounts = accounts;
        }

        public UserEntity(string username, string email, string phone, List<AccountEntity> accounts)
        {
            this.Username = username;
            this.Email = email;
            this.Phone = phone;
            this.Accounts = accounts;
        }

        public UserEntity()
        {
            
        }
    }
}