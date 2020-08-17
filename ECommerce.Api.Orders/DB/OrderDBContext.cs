using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.DB
{
    public class OrderDBContext :DbContext
    {
        public OrderDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
    }
}
