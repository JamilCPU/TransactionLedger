namespace Backend.api.dtos
{
    public class SuccessfulLoginDto
    {
        public string Message { get; set; }
        public string JwtToken { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public SuccessfulLoginDto(string message, string token, string username, string password)
        {
            this.Message = message;
            this.JwtToken = token;
            this.Username = username;
            this.Password = password;
        }
    }
} 