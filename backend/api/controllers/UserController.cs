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

        }
    }
}