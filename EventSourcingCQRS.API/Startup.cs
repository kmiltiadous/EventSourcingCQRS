using EventSourcingCQRS.Application.Configuration;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventSourcingCQRS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var infrastructureModule = new InfrastructureModule(services);
            infrastructureModule.Configure();
            var eventHandlersModule = new EventHandlersModule(services);
            eventHandlersModule.Configure();
            var storageModule = new StorageModule(services);
            storageModule.Configure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            IEventStoreConnection conn, 
            IRepository<Product> productRepository,
            IRepository<Customer> customerRepository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            StorageModule.InitializeReadData(conn, productRepository, customerRepository);
        }
    }
}
