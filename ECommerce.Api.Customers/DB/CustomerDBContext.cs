using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.DB
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions contextOptions) :base(contextOptions)
        {
                
        }

        public DbSet<DB.Customer> Customers { get; set; }

    }
}
