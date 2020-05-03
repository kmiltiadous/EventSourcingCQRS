using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Client.Models;

namespace Mobile.Client.Services
{
    public interface IProductService
    { 
        Task<IEnumerable<Product>> GetProducts();
    }
}
