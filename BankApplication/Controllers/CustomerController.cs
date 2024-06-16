using BankApplication.Intereface;
using BankApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{
    
    public class CustomerController : BaseController
    {
        private readonly ICustomer _customer;

        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }
        [HttpPost]
        public IActionResult Create(CustomerRequestModel customer)
        {
            return Ok(_customer.Create(customer));
        }
    }
}
