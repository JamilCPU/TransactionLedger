using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;

namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        [HttpPost("{id}/deposit")]
        public IActionResult Deposit(int id, [FromBody] AccountRequestDto accountRequestDto)
        {
            //checks:
            //does id exist
            // amount > 0 

            return Ok();
            //execute call business logic
        }

        [HttpPost("{id}/withdraw")]
        public IActionResult Withdraw(int id, [FromBody] AccountRequestDto accountRequestDto)
        {
            //checks:
            //does id exist
            // amount > 0 
            //withdrawing should not exceed balance

            //execute call business logic
            return Ok();
        }

        [HttpPost("transfer")]
        public IActionResult Transfer(int amount, int fromAccountId, int toAccountId)
        {

            //checks:
            //reference deposit checks
            //reference withdraw checks


            //execute call business logic
            return Ok();
        }
    }
}