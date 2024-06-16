using BankApplication.Common;
using BankApplication.Models;

namespace BankApplication.Intereface
{
    public interface ICustomer
    {
        ResponseModel Create(CustomerRequestModel customer);
    }
}
