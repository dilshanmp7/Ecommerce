using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interface;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchInterface _searchInterface;

        public SearchController(ISearchInterface searchInterface)
        {
            _searchInterface = searchInterface;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync([FromBody]SearchTerms terms)
        {
            var result = await _searchInterface.SerchAsync(terms.CustomerId);
            if (result.IsSucess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }

    }
}
