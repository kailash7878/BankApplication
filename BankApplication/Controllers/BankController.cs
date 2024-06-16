using BankApplication.Common;
using BankApplication.Intereface;
using BankApplication.Models;
using BankApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{
    public class BankController : BaseController
    {
        private readonly IBank _bank;

        public BankController(IBank bank)
        {
            _bank = bank;
        }

        [HttpPost]
        public IActionResult Create(BankRequestModel bank)
        {
            return Ok(_bank.Create(bank));
        }

        [HttpGet]
        public ResponseModel ListCustomerByBank(int bankId)
        {
            return _bank.ListCustomerByBank(bankId);
        }
    }
}
