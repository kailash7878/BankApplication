namespace BankApplication.Models
{
    public class AccountRequestModel
    {
        public int CustomerId {  get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance {  get; set; }
        public decimal InterestRate { get; set; }
    }
}
