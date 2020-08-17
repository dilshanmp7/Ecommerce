using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product = ECommerce.Api.Products.Models.Product;

namespace ECommerce.Api.Products.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly ProductDbContext _dbContext;
        private readonly ILogger<ProductProvider> _logger;
        private readonly IMapper _mapper;

        public ProductProvider(ProductDbContext dbContext,ILogger<ProductProvider> logger,IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Produts.Any())
            {
                _dbContext.Produts.Add(new DB.Product() {Id = 1, Name = "Product 1", Price = 100, Inventory = 10});
                _dbContext.Produts.Add(new DB.Product() { Id = 2, Name = "Product 2", Price = 200, Inventory = 20 });
                _dbContext.Produts.Add(new DB.Product() { Id = 3, Name = "Product 3", Price = 300, Inventory = 30 });
                _dbContext.Produts.Add(new DB.Product() { Id = 4, Name = "Product 4", Price = 400, Inventory = 40});
                _dbContext.SaveChanges();

            }

        }

        public async Task<(bool IsSucess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Produts.ToListAsync();
                if (products != null && products.Any())
                {
                   var result= _mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                   return (true, result, null);
                }
                return (false,null , "Not Found");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.ToString());
            }
        }

        public async Task<(bool IsSucess, Product product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _dbContext.Produts.FirstOrDefaultAsync(a=>a.Id==id);
                if (product != null)
                {
                    var result = _mapper.Map<DB.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.ToString());
            }
        }
    }
}
