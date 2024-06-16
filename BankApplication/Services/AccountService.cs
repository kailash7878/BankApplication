using BankApplication.Common;
using BankApplication.Data.models;
using BankApplication.Data.Models;
using BankApplication.Intereface;
using BankApplication.Models;

namespace BankApplication.Services
{
    public class AccountService : IAccount
    {
        private readonly IGenericRepository<Account> _repository;
        private readonly IGenericRepository<AccountLedger> _accountLedger;
        private readonly IGenericRepository<Customer> _customer;

        public AccountService(IGenericRepository<Account> repository, IGenericRepository<AccountLedger> accountLedger, IGenericRepository<Customer> customer)
        {
            _repository = repository;
            _accountLedger = accountLedger;
            _customer = customer;
        }
        public ResponseModel Create(AccountRequestModel account)
        {
            var errors = new List<string>();
            if (account.CustomerId <= 0)
            {
                errors.Add("Please select customer name.");
            }
            if (string.IsNullOrWhiteSpace(account.AccountNumber))
            {
                errors.Add("Account number can not be blank.");
            }
            if (account.Balance < 0)
            {
                errors.Add("Not Allow nagative account balance.");
            }
            if (account.InterestRate < 0)
            {
                errors.Add("Not Allow nagative interest rate.");
            }

            if (errors.Any())
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Data = errors,
                    message = "Filed"
                };
            }

            _repository.Add(new Account
            {
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                InterestRate = account.InterestRate,
            });

            return new ResponseModel
            {
                IsSuccess = true,
                Data = "Success",
                message = "Success"
            };
        }

        public ResponseModel Transaction(TransactionRequestModel model)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(model.CreditAccountNumber))
            {
                errors.Add("Invalid credit account number.");
            }

            if (string.IsNullOrWhiteSpace(model.DebitAccountNumber))
            {
                errors.Add("Invalid debit account number.");
            }

            if (model.Amount < 0)
            {
                errors.Add("Invalid credit Amount");
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

            var creditAccount = GetAccount(model.CreditAccountNumber);
            if (creditAccount == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Data = "Invalid credit Account number.",
                    message = "Failed"
                };
            }

            var debitAccount = GetAccount(model.DebitAccountNumber);
            if (debitAccount == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Data = "Invalid debit Account number.",
                    message = "Failed"
                };
            }

            if (debitAccount.Balance < model.Amount)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Data = "Insufficient account .",
                    message = "Failed"
                };
            }

            creditAccount.Balance = creditAccount.Balance + model.Amount;
            debitAccount.Balance = debitAccount.Balance - model.Amount;

            _repository.Update(creditAccount);
            _repository.Update(debitAccount);
            _accountLedger.Add(new AccountLedger
            {
                Id = 0,
                CrAccountnumber = creditAccount.AccountNumber,
                DrAccountNumbebr = debitAccount.AccountNumber,
                CrAmount = model.Amount,
                DrAmount = model.Amount
            });

            return new ResponseModel
            {
                IsSuccess = true,
                Data = "Success",
                message = "Success"
            };
        }

        public ResponseModel CalculateInterest(string accountNumber)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Data = "Bed request",
                    message = "Failed"
                };
            }

            var account = _repository.Select(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Data = "Invalid account number.",
                    message = "Failed"
                };
            }

            var interest = ((account.Balance * account.InterestRate) / 100);
            account.Balance = (account.Balance + ((account.Balance * account.InterestRate) / 100));
            _repository.Update(account);

            _accountLedger.Add(new AccountLedger
            {
                Id = 0,
                CrAccountnumber = accountNumber ,
                DrAccountNumbebr = null,
                CrAmount = interest ?? 0,
                DrAmount = 0
            });

            return new ResponseModel
            {
                IsSuccess = true,
                Data = "Success",
                message = "Success"
            };
        }
        public ResponseModel ListAccountByCustomer(int customerId)
        {
            var accunts = _customer.GetAllwithInclude(x => x.Id == customerId, x=>x.Accounts).ToList();
            return new ResponseModel
            {
                IsSuccess = true,
                Data = accunts,
                message = "Success"
            };
        }

        private bool ValidateAccountNumber(string accountNumber)
        {
            var data = _repository.Select(x => x.AccountNumber == accountNumber);
            if (data == null)
            {
                return false;
            }
            return true;
        }

        private Account GetAccount(string accountNumer)
        {
            return _repository.Select(x => x.AccountNumber == accountNumer);
        }
    }
}
