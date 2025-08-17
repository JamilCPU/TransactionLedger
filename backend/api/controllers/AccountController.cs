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
            if (accountRequestDto.Amount < 0)
            {
                return BadRequest("Initial amount cannot be negative");
            }
            var account = await _accountService.CreateAccount(accountRequestDto);
            return Ok(account);
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] AccountRequestDto accountRequestDto)
        {
            if (accountRequestDto.Amount <= 0)
            {
                return BadRequest("Deposit amount must be greater than 0");
            }
            var updatedAccount = await _accountService.Deposit(id, accountRequestDto.Amount);
            return Ok(updatedAccount);
        }

        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] AccountRequestDto accountRequestDto)
        {
            if (accountRequestDto.Amount <= 0)
            {
                return BadRequest("Withdrawal amount must be greater than 0");
            }
            var updatedAccount = await _accountService.Withdraw(id, accountRequestDto.Amount);
            return Ok(updatedAccount);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequestDto transferRequestDto)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet("getAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccount(id);
            return Ok();
        }

        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
    }


}