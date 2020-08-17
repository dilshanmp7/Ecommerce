using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController :ControllerBase
    {
        private readonly IProductProvider _productProvider;

        public ProductController(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result =await _productProvider.GetProductsAsync();
            if (result.IsSucess)
            {
                return Ok(result.products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _productProvider.GetProductAsync(id);
            if (result.IsSucess)
            {
                return Ok(result.product);
            }
            return NotFound();
        }

    }
}
