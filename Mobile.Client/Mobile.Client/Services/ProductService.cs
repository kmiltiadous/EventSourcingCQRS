using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Client.Models;

namespace Mobile.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericService genericService;
        public ProductService(IGenericService genericService)
        {
            this.genericService = genericService;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = $"{ApiEndpoints.ProductsEndpoint}"
            };
            var result = await genericService.GetAsync<ActionResponse<IEnumerable<Product>>>(builder.ToString());

            return !result.WasSuccessful ? new List<Product>() : result.Value;
        }
    }
}
