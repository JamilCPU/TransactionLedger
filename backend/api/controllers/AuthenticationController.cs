using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;
using Backend.service.intrface;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (userDto.Username == null || userDto.Password == null || userDto.Email == null || userDto.Phone == null)
            {
                return BadRequest("Username, password, email, and phone are required");
            }
            var user = await _userService.CreateUser(userDto);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = await _userService.Login(userDto);
            return Ok(user);
        }

        
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
    }

}