[ApiController]
[Route("transaction")]
public class TransactionController : ControllerBase{
    [HttpGet("{id}/getAllTransactions")]
    public IActionResult getAllTransactions(int id, TransactionRequestDto transactionRequestDto){
        //checks:
        //does id exist

        //execute call business logic
    }

    [HttpGet("{id}/getTransactionById")]
    public IActionResult getTransactionById(int id, TransactionRequestDto transactionRequestDto){
        //checks:
        //does id exist

        //execute call business logic
    }
}