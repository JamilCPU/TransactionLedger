using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;

namespace Backend.api.controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult createUser([FromBody] UserDto userDto)
        {
            //checks:
            //unique username
            //unique id

            return Ok();
        }
    }
}