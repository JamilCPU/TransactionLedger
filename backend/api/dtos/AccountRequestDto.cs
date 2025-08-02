namespace Backend.api.dtos
{
    public class AccountRequestDto
    {
        public int balance { get; set; }

        public AccountRequestDto(int balance)
        {
            this.balance = balance;
        }
    }
}