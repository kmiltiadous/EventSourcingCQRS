using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingCQRS.Models;

namespace EventSourcingCQRS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericService genericService;

        public CustomerService(IGenericService genericService)
        {
            this.genericService = genericService;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = $"{ApiEndpoints.CustomersEndpoint}"
            };
            var result = await genericService.GetAsync<ActionResponse<IEnumerable<Customer>>>(builder.ToString());

            return !result.WasSuccessful ? new List<Customer>() : result.Value;
        }
    }

    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();
    }
}
