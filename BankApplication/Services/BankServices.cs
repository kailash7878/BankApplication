using BankApplication.Common;
using BankApplication.Data;
using BankApplication.Data.models;
using BankApplication.Intereface;
using BankApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BankApplication.Services
{
    public class BankServices : IBank
    {
        private readonly IGenericRepository<Bank> _bank;
        private readonly IGenericRepository<Customer> _customer;
        public BankServices(IGenericRepository<Bank> bank, IGenericRepository<Customer> customer)
        {
            _bank = bank;
            _customer = customer;
        }
        public ResponseModel Create(BankRequestModel bank)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(bank.Name))
            {
                errors.Add("Bank name can not be blank.");
            }
            if (string.IsNullOrWhiteSpace(bank.Address))
            {
                errors.Add("Bank address can not be blank.");
            }

            if (IsBankNameExists(bank.Name)) 
            {
                errors.Add("Bank name exists.");
            }
            if (errors.Any())
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Data = errors,
                    message = "Failed"
                };
            }

            if (IsBankNameExists(bank.Name))
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Data = "Bank  Name Exists.",
                    message = "Failed"
                };
            }

            _bank.Add(new Bank() 
            { 
                Id =0,
                Name = bank.Name,
                Addrerss= bank.Address
            });

            return new ResponseModel
            {
                IsSuccess = true,
                Data = "Bank Created",
                message = "Success"
            };
        }

        public ResponseModel ListCustomerByBank(int bankId)
        {
            var customers = _customer.GetAllwithInclude(x => x.BankId == bankId,x=>x.Accounts).ToList();
            return new ResponseModel
            {
                IsSuccess = true,
                Data = customers,
                message = "Success"
            };
        }
        private bool IsBankNameExists(string bankName)
        {
            var data = _bank.Select(x => x.Name == bankName);
            if (data == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
