using Backend.data.entities;

namespace Backend.api.dtos
{
    public class AccountRequestDto
    {
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public AccountEntity.AccountTypeEnum AccountType { get; set; }

        public AccountRequestDto(decimal amount, int userId, AccountEntity.AccountTypeEnum accountType)
        {
            this.Amount = amount;
            this.UserId = userId;
            this.AccountType = accountType;
        }
    }
}