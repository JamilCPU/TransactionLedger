using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;
using Backend.service.intrface;

namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        [HttpPost("new")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (userDto.Username == null || userDto.Password == null || userDto.Email == null || userDto.Phone == null)
            {
                return BadRequest("Username, password, email, and phone are required");
            }
            var user = await _userService.CreateUser(userDto);
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            if (userDto.Username == null || userDto.Password == null || userDto.Email == null || userDto.Phone == null)
            {
                return BadRequest("Username, password, email, and phone are required");
            }
            var updatedUser = await _userService.UpdateUser(userId, userDto);
            return Ok(updatedUser);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (userId == null)
            {
                return BadRequest("Error: User Id is Null!");
            }
            await _userService.DeleteUser(userId);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }





        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}