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
            try
            {
                if (transactionDto.Amount <= 0)
                {
                    return BadRequest("Transaction amount must be greater than 0");
                }
                var transaction = await _transactionService.CreateTransaction(transactionDto);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateTransaction error: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var transaction = await _transactionService.GetTransactionById(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTransactionById error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the transaction");
            }
        }

        [HttpGet("getAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            try
            {
                var transactions = await _transactionService.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllTransactions error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving transactions");
            }
        }

        [HttpGet("account/{accountId}/transactions")]
        public async Task<IActionResult> GetTransactionsByAccountId(int accountId)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByAccountId(accountId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTransactionsByAccountId error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving transactions for the account");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                await _transactionService.DeleteTransaction(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteTransaction error: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the transaction");
            }
        }

        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
    }
}