using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.DB
{
    public class ProductDbContext : DbContext
    {

        public ProductDbContext(DbContextOptions contextOptions) :base(contextOptions)
        {
                
        }


        public DbSet<Product> Produts { get; set; }


    }
}
