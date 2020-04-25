using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventStore.ClientAPI;
using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.Persistence;
using EventSourcingCQRS.Domain.PubSub;
using EventSourcingCQRS.Domain.EventStore;
using EventSourcingCQRS.Domain.Persistence.EventStore;
using MongoDB.Driver;
using EventSourcingCQRS.ReadModel.Persistence;
using ReadCart = EventSourcingCQRS.ReadModel.Models.Cart;
using ReadCartItem = EventSourcingCQRS.ReadModel.Models.CartItem;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventSourcingCQRS.Application.Services;
using EventSourcingCQRS.Application.PubSub;
using EventSourcingCQRS.Application.Handlers;
using MediatR;
using EventSourcingCQRS.Application.Commands;
using EventSourcingCQRS.ReadModel.Models;
using Microsoft.AspNetCore.Mvc;
using Cart = EventSourcingCQRS.Domain.CartModule.Cart;

namespace EventSourcingCQRS
{
    public class Startup
    {
        private const string ReadModelDBName = "ReadModel";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMediatR(typeof(GetCartDetailsCommand));
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton(x => EventStoreConnection.Create(new Uri("tcp://127.0.0.1:1113")));
            services.AddTransient<ITransientDomainEventPublisher>(s => TransientDomainEventPubSubFactory.CreateInstance(s));
            services.AddTransient<ITransientDomainEventSubscriber, TransientDomainEventPubSub>();
            services.AddTransient<IRepository<Cart, CartId>, EventSourcingRepository<Cart, CartId>>();
            services.AddSingleton<IEventStore, EventStoreEventStore>();
            services.AddSingleton(x => new MongoClient("mongodb://localhost:27017"));
            services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(ReadModelDBName));
            services.AddTransient<IReadOnlyRepository<ReadCart>, MongoDBRepository<ReadCart>>();
            services.AddTransient<IRepository<ReadCart>, MongoDBRepository<ReadCart>>();
            services.AddTransient<IReadOnlyRepository<ReadCartItem>, MongoDBRepository<ReadCartItem>>();
            services.AddTransient<IRepository<ReadCartItem>, MongoDBRepository<ReadCartItem>>();
            services.AddTransient<IReadOnlyRepository<Product>, MongoDBRepository<Product>>();
            services.AddTransient<IRepository<Product>, MongoDBRepository<Product>>();
            services.AddTransient<IReadOnlyRepository<Customer>, MongoDBRepository<Customer>>();
            services.AddTransient<IRepository<Customer>, MongoDBRepository<Customer>>();
            services.AddTransient<IDomainEventHandler<CartId, CartCreatedEvent>, CartUpdater>();
            services.AddTransient<IDomainEventHandler<CartId, ProductAddedEvent>, CartUpdater>();
            services.AddTransient<IDomainEventHandler<CartId, ProductQuantityChangedEvent>, CartUpdater>();
            services.AddTransient<ICartWriter, CartWriter>();
            services.AddTransient<ICartReader, CartReader>();
        }

        private class TransientDomainEventPubSubFactory
        {
            public static TransientDomainEventPubSub CreateInstance(IServiceProvider provider)
            {
                var publisher = new TransientDomainEventPubSub();
                publisher.AddSubscriber<CartCreatedEvent>(async @event => await provider.GetService<IDomainEventHandler<CartId, CartCreatedEvent>>().HandleAsync(@event));
                publisher.AddSubscriber<ProductAddedEvent>(async @event => await provider.GetService<IDomainEventHandler<CartId, ProductAddedEvent>>().HandleAsync(@event));
                publisher.AddSubscriber<ProductQuantityChangedEvent>(async @event => await provider.GetService<IDomainEventHandler<CartId, ProductQuantityChangedEvent>>().HandleAsync(@event));

                return publisher;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IEventStoreConnection conn, IRepository<Product> productRepository,
            IRepository<Customer> customerRepository, MongoClient mongoClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=CartsV2}/{action=IndexAsync}/{id?}");
            });

            conn.ConnectAsync().Wait();

            if (!productRepository.FindAllAsync(x => true).Result.Any() &&
                !customerRepository.FindAllAsync(x => true).Result.Any())
            {
                SeedReadModel(productRepository, customerRepository);
            }
        }

        private void SeedReadModel(IRepository<Product> productRepository, IRepository<Customer> customerRepository)
        {
            var insertingProducts = new [] {
                new Product
                {
                    Id = $"Product-{Guid.NewGuid().ToString()}",
                    Name = "Laptop"
                },
                new Product
                {
                    Id = $"Product-{Guid.NewGuid().ToString()}",
                    Name = "Smartphone"
                },
                new Product
                {
                    Id = $"Product-{Guid.NewGuid().ToString()}",
                    Name = "Gaming PC"
                },
                new Product
                {
                    Id = $"Product-{Guid.NewGuid().ToString()}",
                    Name = "Microwave oven"
                },
            }
            .Select(x => productRepository.InsertAsync(x));

            var insertingCustomers = new Customer[] {
                new Customer
                {
                    Id = $"Customer-{Guid.NewGuid().ToString()}",
                    Name = "Andrea"
                },
                new Customer
                {
                    Id = $"Customer-{Guid.NewGuid().ToString()}",
                    Name = "Martina"
                },
            }
            .Select(x => customerRepository.InsertAsync(x));

            Task.WaitAll(insertingProducts.Union(insertingCustomers).ToArray());
        }
    }
}
