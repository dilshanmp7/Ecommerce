using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductProvider
    {
        Task<(bool IsSucess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSucess, Product product, string ErrorMessage)> GetProductAsync(int id);



    }
}
