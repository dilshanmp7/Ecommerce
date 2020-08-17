using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProvider _orderProvider;

        public OrderController(IOrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomer(int customerId)
        {
            var result = await _orderProvider.GetAllOrderByCusomerId(customerId);
            if (result.IsSucess)
                return Ok(result.orders);
            return NotFound();
        }

    }
}
