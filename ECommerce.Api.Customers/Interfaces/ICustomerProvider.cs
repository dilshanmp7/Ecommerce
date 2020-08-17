using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {

        Task<(bool IsSucess, IEnumerable<Models.Customer> customers, string errorMessage)> GetAllCustomers();
        Task<(bool IsSucess, Models.Customer customer, string errorMessage)> GetCustomerById(int id);

    }
}
