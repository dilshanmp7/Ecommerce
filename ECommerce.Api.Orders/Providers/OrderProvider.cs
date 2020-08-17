using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Orders.DB;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order = ECommerce.Api.Orders.DB.Order;

namespace ECommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrderDBContext _dbContext;
        private readonly ILogger<OrderProvider> _logger;
        private readonly IMapper _mapper;

        public OrderProvider(OrderDBContext dbContext,ILogger<OrderProvider> logger,IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Orders.Any())
            {
                _dbContext.Orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quntity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _dbContext.Orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quntity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _dbContext.Orders.Add(new Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quntity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quntity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSucess, IEnumerable<Models.Order> orders, string errorMessage)> GetAllOrderByCusomerId(int customerId)
        {
            try
            {
                var orders = await _dbContext.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Order>,
                        IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }
    }
}
