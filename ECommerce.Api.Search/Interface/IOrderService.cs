using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interface
{
    public interface IOrderService
    {
        Task<(bool IsSucess, IEnumerable<Order> orders,string errorMessage)> GetOrderAsync(int customerId);
    }
}
