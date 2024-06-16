using BankApplication.Common;
using BankApplication.Data.models;
using BankApplication.Intereface;
using BankApplication.Models;
using System.Text.RegularExpressions;

namespace BankApplication.Services
{
    public class CustomerService : ICustomer
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IGenericRepository<Account> _account;

        public CustomerService(IGenericRepository<Customer> repository, IGenericRepository<Account> account)
        {
            _repository = repository;
            _account = account;
        }
        public ResponseModel Create(CustomerRequestModel customer)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                errors.Add("Customer name can not bb bannk.");
            }

            if (customer.BankId <= 0)
            {
                errors.Add("Please select bank name.");
            }

            if (!validateEmail(customer.Email))
            {
                errors.Add("Invalid customer email.");
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

            _repository.Add(new Customer()
            {
                Id = 0,
                Name = customer.Name,
                Email = customer.Email,
                BankId = customer.BankId,
            });

            return new ResponseModel
            {
                IsSuccess = true,
                Data = "Success",
                message = "Success"
            };
        }

        private bool validateEmail(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }
    }
}
