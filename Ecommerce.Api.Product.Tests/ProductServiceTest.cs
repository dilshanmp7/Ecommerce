using System;
using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
using Xunit;

namespace Ecommerce.Api.Product.Tests
{
    public class ProductServiceTest
    {

        private void CreateProduct(ProductDbContext dbContext)
        {
            for (int i = 10; i < 20; i++)
            {
                dbContext.Produts.Add(new ECommerce.Api.Products.DB.Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }

            dbContext.SaveChanges();
        }


        [Fact]
        public async void GetProductsReturnAllProducts()
        {
            var options=new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase("ProductDB").Options;

            var dbContext =new ProductDbContext(options);
            CreateProduct(dbContext);


            var productProfile =new ProductProfile();
            var configuration = new MapperConfiguration(cfg=>cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productProvider =new ProductProvider(dbContext,null,mapper);

            var product = await productProvider.GetProductsAsync();

            Assert.True(product.IsSucess);
            Assert.True(product.products.Any());
            Assert.Null(product.ErrorMessage);

            dbContext.Produts = null;
        }


        [Fact]
        public async void GetProductsReturnOneProduct()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase("ProductDB").Options;

            var dbContext = new ProductDbContext(options);
            CreateProduct(dbContext);


            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productProvider = new ProductProvider(dbContext, null, mapper);

            var product = await productProvider.GetProductAsync(10);

            Assert.True(product.IsSucess);
            Assert.NotNull(product.product);
            Assert.Equal(10,product.product.Id);
            Assert.Null(product.ErrorMessage);
        }

    }
}
