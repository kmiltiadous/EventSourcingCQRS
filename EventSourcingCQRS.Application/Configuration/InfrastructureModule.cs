using EventSourcingCQRS.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcingCQRS.Application.Configuration
{
    public class InfrastructureModule :IConfigureModule
    {
        private readonly IServiceCollection services;
        public InfrastructureModule(IServiceCollection services)
        {
            this.services = services;
        }

        public void Configure()
        {
            services.AddMediatR(typeof(AddProductCommand));
        }
    }
}
