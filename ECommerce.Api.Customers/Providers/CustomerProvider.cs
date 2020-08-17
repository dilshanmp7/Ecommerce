using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Customers.DB;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider :ICustomerProvider
    {
        private readonly CustomerDBContext _dbContext;
        private readonly ILogger<CustomerProvider> _logger;
        private readonly IMapper _mapper;

        public CustomerProvider(CustomerDBContext dbContext,ILogger<CustomerProvider> logger,IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.Add(new Customer() {Id = 1, Name = "Dilshan", Address = "Esberns Alle 1,1.4"});
                _dbContext.Customers.Add(new Customer() { Id = 2, Name = "Ama", Address = "Esberns Alle 1,1.5" });
                _dbContext.Customers.Add(new Customer() { Id = 3, Name = "Ashan", Address = "Kelaniya, Sri lanka" });
                _dbContext.SaveChanges();
            }
           
        }


        public async Task<(bool IsSucess, IEnumerable<Models.Customer> customers, string errorMessage)> GetAllCustomers()
        {
            var result= _mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(await _dbContext.Customers.ToListAsync());
            if (result != null && result.Any())
            {
                return (true, result, null);
            }

            return (false, null, "Not found");
        }

        public async Task<(bool IsSucess, Models.Customer customer, string errorMessage)> GetCustomerById(int id)
        {
            var result = _mapper.Map<DB.Customer , Models.Customer>(await _dbContext.Customers.FirstOrDefaultAsync(a=>a.Id.Equals(id)));
            if (result != null )
            {
                return (true, result, null);
            }
            return (false, null, "Not found");
        }
    }
}
