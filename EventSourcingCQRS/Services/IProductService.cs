using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingCQRS.Models;

namespace EventSourcingCQRS.Services
{
    public interface IProductService
    { 
        Task<IEnumerable<Product>> GetProducts();
    }
}
