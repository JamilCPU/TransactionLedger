using Backend.data.entities;
namespace Backend.api.dtos
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<AccountEntity> Accounts { get; set; }
        public UserInfoDto(int Id, string username, string email, string phone, List<AccountEntity> accounts)
        {
            this.Id = Id;
            this.Username = username;
            this.Email = email;
            this.Phone = phone;
            this.Accounts = accounts;
        }
    }
}