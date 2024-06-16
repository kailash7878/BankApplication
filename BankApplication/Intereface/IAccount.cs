using BankApplication.Common;
using BankApplication.Models;

namespace BankApplication.Intereface
{
    public interface IAccount
    {
        ResponseModel Create(AccountRequestModel account);
        ResponseModel Transaction(TransactionRequestModel model);
        ResponseModel CalculateInterest(string accountNumber);
        ResponseModel ListAccountByCustomer(int customerId);

    }
}
