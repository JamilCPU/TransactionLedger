namespace Backend.api.dtos
{
    public class TransferRequestDto
    {
        public decimal Amount { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }

        public TransferRequestDto(decimal amount, int fromAccountId, int toAccountId)
        {
            this.Amount = amount;
            this.FromAccountId = fromAccountId;
            this.ToAccountId = toAccountId;
        }
    }
}