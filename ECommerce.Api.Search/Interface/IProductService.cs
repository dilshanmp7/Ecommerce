using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interface
{
    public interface IProductService
    {
        Task<(bool IsSucess, IEnumerable<Product> products, string errorMessage)> GetProductAsync();

    }
}
