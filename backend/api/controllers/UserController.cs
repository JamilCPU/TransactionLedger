using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;
using Backend.service.impl;

namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if(userDto.Username == null || userDto.Password == null || userDto.Email == null || userDto.Phone == null)
            {
                return BadRequest("Username, password, email, and phone are required");
            }
            UserService userService = new UserService();
            await userService.CreateUser(userDto);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            return Ok();
        }

    }
}