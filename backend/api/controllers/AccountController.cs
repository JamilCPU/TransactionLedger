[ApiController]
[Route("account")]
public class AccountController : ControllerBase{
    [HttpPost("{id}/deposit")]
    public IActionResult deposit(int id, [FromBody] AccountRequestDto accountRequestDto){
        //checks:
        //does id exist
        // amount > 0 

        
        //execute call business logic
    }

    [HttpPost("{id}/withdraw")]
    public IActionResult withdraw(int id, [FromBody] AccountRequestDto accountRequestDto){
        //checks:
        //does id exist
        // amount > 0 
        //withdrawing should not exceed balance

        //execute call business logic
    }

    [HttpPost("transfer")]
    public IActionResult transfer(int amount, int fromAccountId, int toAccountId){

        //checks:
        //reference deposit checks
        //reference withdraw checks


        //execute call business logic
    }
}
