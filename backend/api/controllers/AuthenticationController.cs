namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if (registerRequestDto.Username == null || registerRequestDto.Password == null || registerRequestDto.Email == null || registerRequestDto.Phone == null)
            {
                return BadRequest("Username, password, email, and phone are required");
            }
            var user = await _userService.Register(registerRequestDto);
            return Ok(user);
        }
    }
}
}