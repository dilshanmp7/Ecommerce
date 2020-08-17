using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool IsSucess, IEnumerable<Order> orders, string errorMessage)> GetAllOrderByCusomerId(int customerId);

    }
}
