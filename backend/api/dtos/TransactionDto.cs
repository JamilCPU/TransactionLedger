namespace Backend.api.dtos
{
    public class TransactionRequestDto
    {
        public decimal amount { get; set; }

        public TransactionRequestDto(decimal amount)
        {
            this.amount = amount;
        }
    }
}