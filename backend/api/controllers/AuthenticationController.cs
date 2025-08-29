using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;
using Backend.service.intrface;


namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                if (userDto.Username == null || userDto.Password == null || userDto.Email == null || userDto.Phone == null)
                {
                    return BadRequest("Username, password, email, and phone are required");
                }
                var user = await _userService.CreateUser(userDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register error: {ex.Message}");
                return StatusCode(500, "An error occurred during registration");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                Console.WriteLine(loginDto.Username);
                Console.WriteLine(loginDto.Password);
                if (loginDto.Username == null || loginDto.Password == null)
                {
                    return BadRequest("Username and password are required");
                }
                var loginResult = await _userService.Login(loginDto.Username, loginDto.Password);
                return Ok(loginResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return Unauthorized("Invalid username or password");
            }
        }

        
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
    }

}