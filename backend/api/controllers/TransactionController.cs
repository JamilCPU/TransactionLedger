using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;
using Backend.service.intrface;

namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        [HttpPost("new")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto transactionDto)
        {
            if (transactionDto.Amount <= 0)
            {
                return BadRequest("Transaction amount must be greater than 0");
            }
            var transaction = await _transactionService.CreateTransaction(transactionDto);
            return Ok(transaction);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpGet("getAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpGet("account/{accountId}/transactions")]
        public async Task<IActionResult> GetTransactionsByAccountId(int accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountId(accountId);
            return Ok(transactions);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _transactionService.DeleteTransaction(id);
            return Ok();
        }

        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
    }
}