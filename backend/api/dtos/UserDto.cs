namespace Backend.api.dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public UserDto(string username, string password, string email, string phone)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Phone = phone;
        }
    }
}