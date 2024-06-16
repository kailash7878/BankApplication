using BankApplication.Common;
using BankApplication.Data.models;
using BankApplication.Models;

namespace BankApplication.Intereface
{
    public interface IBank
    {
        ResponseModel Create(BankRequestModel bank);
        ResponseModel ListCustomerByBank(int bankId);
    }
}
