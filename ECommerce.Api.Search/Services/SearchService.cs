using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interface;

namespace ECommerce.Api.Search.Services
{
    public class SearchService :ISearchInterface
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public SearchService(IOrderService orderService,IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public async Task<(bool IsSucess, dynamic SearchResults)> SerchAsync(int custoimerId)
        {
            var ordersResult = await _orderService.GetOrderAsync(custoimerId);
            var productResult = await _productService.GetProductAsync();

            if (ordersResult.IsSucess)
            {

                foreach (var order in ordersResult.orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSucess
                            ? productResult.products.FirstOrDefault(a => a.Id.Equals(item.ProductId))?.Name
                            : "PÅroduct Information is not available";
                    }
                }


                var result = new
                {
                    orders= ordersResult.orders
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}
