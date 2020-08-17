using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider _cusomerProvider;


        public CustomerController(ICustomerProvider cusomerProvider)
        {
            _cusomerProvider = cusomerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _cusomerProvider.GetAllCustomers();
            if (result.IsSucess)
                return Ok(result.customers);
            return NotFound();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await _cusomerProvider.GetCustomerById(id);
            if (result.IsSucess)
                return Ok(result.customer);
            return NotFound();
        }

    }
}
