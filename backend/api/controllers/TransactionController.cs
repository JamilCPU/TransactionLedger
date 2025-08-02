using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;

namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        [HttpGet("{id}/getAllTransactions")]
        public IActionResult getAllTransactions(int id, TransactionRequestDto transactionRequestDto)
        {
            //checks:
            //does id exist

            //execute call business logic
            return Ok();
        }

        [HttpGet("{id}/getTransactionById")]
        public IActionResult getTransactionById(int id, TransactionRequestDto transactionRequestDto)
        {
            //checks:
            //does id exist

            //execute call business logic
            return Ok();
        }
    }
}