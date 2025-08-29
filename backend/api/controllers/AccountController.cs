using Microsoft.AspNetCore.Mvc;
using Backend.api.dtos;
using Backend.service.intrface;

namespace Backend.api.controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        [HttpPost("new")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequestDto accountRequestDto)
        {
            try
            {
                if (accountRequestDto.Amount < 0)
                {
                    return BadRequest("Initial amount cannot be negative");
                }
                var account = await _accountService.CreateAccount(accountRequestDto);
                return Ok(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateAccount error: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the account");
            }
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] AccountRequestDto accountRequestDto)
        {
            try
            {
                if (accountRequestDto.Amount <= 0)
                {
                    return BadRequest("Deposit amount must be greater than 0");
                }
                var updatedAccount = await _accountService.Deposit(id, accountRequestDto.Amount);
                return Ok(updatedAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deposit error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the deposit");
            }
        }

        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] AccountRequestDto accountRequestDto)
        {
            try
            {
                if (accountRequestDto.Amount <= 0)
                {
                    return BadRequest("Withdrawal amount must be greater than 0");
                }
                var updatedAccount = await _accountService.Withdraw(id, accountRequestDto.Amount);
                return Ok(updatedAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Withdraw error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the withdrawal");
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequestDto transferRequestDto)
        {
            try
            {
                if (transferRequestDto.Amount <= 0)
                {
                    return BadRequest("Transfer amount must be greater than 0");
                }
                if (transferRequestDto.FromAccountId == transferRequestDto.ToAccountId)
                {
                    return BadRequest("Cannot transfer to the same account");
                }
                await _accountService.Transfer(transferRequestDto.FromAccountId, transferRequestDto.ToAccountId, transferRequestDto.Amount);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transfer error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the transfer");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                var account = await _accountService.GetAccountById(id);
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAccountById error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the account");
            }
        }

        [HttpGet("getAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAllAccounts();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllAccounts error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving accounts");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                await _accountService.DeleteAccount(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteAccount error: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the account");
            }
        }

        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
    }
}