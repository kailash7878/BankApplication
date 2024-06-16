using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BankApplication.Data.Models
{
    public class AccountLedger
    {
        public int Id { get; set; }
        public string CrAccountnumber { get; set; }
        public string DrAccountNumbebr { get; set; }
        public decimal CrAmount { get; set; }
        public decimal DrAmount { get; set; }
    }
}
