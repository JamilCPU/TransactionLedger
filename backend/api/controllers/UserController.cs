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
                Console.WriteLine($"CreateUser error: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the user");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            try
            {
                if (userDto.Username == null || userDto.Password == null || userDto.Email == null || userDto.Phone == null)
                {
                    return BadRequest("Username, password, email, and phone are required");
                }
                var updatedUser = await _userService.UpdateUser(userId, userDto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateUser error: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the user");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                if (userId == null)
                {
                    return BadRequest("Error: User Id is Null!");
                }
                await _userService.DeleteUser(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteUser error: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the user");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUserById error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the user");
            }
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllUsers error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving users");
            }
        }

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}