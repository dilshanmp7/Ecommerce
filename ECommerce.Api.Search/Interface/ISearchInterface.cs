using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interface
{
    public interface ISearchInterface
    {
         Task<(bool IsSucess,dynamic SearchResults)> SerchAsync(int custoimerId);
    }
}
