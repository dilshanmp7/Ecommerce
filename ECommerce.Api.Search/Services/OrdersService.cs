using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interface;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;


namespace ECommerce.Api.Search.Services
{
    public class OrdersService :IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrdersService> _logger;

        public OrdersService(IHttpClientFactory httpClientFactory,ILogger<OrdersService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsSucess, IEnumerable<Order> orders, string errorMessage)> GetOrderAsync(int customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrdersService");
                var respond = await client.GetAsync($"api/order/{customerId}");
                if (respond.IsSuccessStatusCode)
                {
                    var content = await respond.Content.ReadAsByteArrayAsync();
                    var options= new JsonSerializerOptions(){PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content,options);
                    return (true, result, null);
                }
                return (false, null, respond.ReasonPhrase);
            }
            catch (Exception e)
            {
               _logger?.LogError(e.ToString());
               return (false, null, e.Message);
            }
        }
    }
}
