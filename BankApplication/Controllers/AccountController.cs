using BankApplication.Intereface;
using BankApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly IAccount _account;
        public AccountController(IAccount account)
        {
            _account = account;
        }
        [HttpPost]
        public IActionResult Create(AccountRequestModel account)
        {
            return Ok(_account.Create(account));
        }

        [HttpPost]
        public IActionResult CreditTransaction(TransactionRequestModel transaction)
        {
            return Ok(_account.Transaction(transaction));
        }

        [HttpPost]
        public IActionResult DebitTransaction(TransactionRequestModel transaction)
        {
            return Ok(_account.Transaction(transaction));
        }

        [HttpPost]
        public IActionResult CalculateInterest(string accountNumber)
        {
            return Ok(_account.CalculateInterest(accountNumber));
        }

        [HttpGet]
        public IActionResult GetAccountByCustomer(int customerId)
        {
            return Ok(_account.ListAccountByCustomer(customerId));
        }
    }
}
