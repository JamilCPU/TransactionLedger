namespace backend.api.dtos
{
    public class AuthResponseDto
    {
        private readonly string AuthToken;
        public string Username { get; set; }


        public AuthResponseDto(string authToken, string username)
        {
            AuthToken = authToken;
            Username = username;
        }
    }
}