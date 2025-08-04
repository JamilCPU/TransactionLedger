namespace Backend.api.dtos
{
    public class UserDto
    {
        // public string Id { get; set; }
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

        // public UserDto(string username, string password, string email, string phone, string Id)
        // {
        //     this.Username = username;
        //     this.Password = password;
        //     this.Email = email;
        //     this.Phone = phone;
        //     this.Id = Id;
        // }
    }
}