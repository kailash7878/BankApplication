namespace BankApplication.Models
{
    public class TransactionRequestModel
    {
        public string CreditAccountNumber { get; set; }
        public string DebitAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
