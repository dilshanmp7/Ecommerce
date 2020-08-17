using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.DB;

namespace ECommerce.Api.Products.Profiles
{
    public class ProductProfile :AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<DB.Product, Models.Product>();
        }


    }
}
