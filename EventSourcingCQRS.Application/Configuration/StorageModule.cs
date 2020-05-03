using System;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingCQRS.Application.Services;
using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.EventStore;
using EventSourcingCQRS.Domain.Persistence;
using EventSourcingCQRS.Domain.Persistence.EventStore;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Cart = EventSourcingCQRS.Domain.CartModule.Cart;
using ReadCart = EventSourcingCQRS.ReadModel.Models.Cart;
using ReadCartItem = EventSourcingCQRS.ReadModel.Models.CartItem;

namespace EventSourcingCQRS.Application.Configuration
{
    public class StorageModule : IConfigureModule
    {
        private const string ReadModelDbName = "ReadModel";
        private readonly IServiceCollection services;

        public StorageModule(IServiceCollection services)
        {
            this.services = services;
        }

        public void Configure()
        {
            services.AddSingleton(x => EventStoreConnection.Create(new Uri("tcp://127.0.0.1:1113")));
            services.AddTransient<IRepository<Cart, CartId>, EventSourcingRepository<Cart, CartId>>();
            services.AddSingleton<IEventStore, EventStoreEventStore>();
            services.AddSingleton(x => new MongoClient("mongodb://localhost:27017"));
            services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(ReadModelDbName));
            services.AddTransient<IReadOnlyRepository<ReadCart>, MongoDBRepository<ReadCart>>();
            services.AddTransient<IRepository<ReadCart>, MongoDBRepository<ReadCart>>();
            services.AddTransient<IReadOnlyRepository<ReadCartItem>, MongoDBRepository<ReadCartItem>>();
            services.AddTransient<IRepository<ReadCartItem>, MongoDBRepository<ReadCartItem>>();
            services.AddTransient<IReadOnlyRepository<Product>, MongoDBRepository<Product>>();
            services.AddTransient<IRepository<Product>, MongoDBRepository<Product>>();
            services.AddTransient<IReadOnlyRepository<Customer>, MongoDBRepository<Customer>>();
            services.AddTransient<IRepository<Customer>, MongoDBRepository<Customer>>();
            services.AddTransient<ICartWriter, CartWriter>();
            services.AddTransient<ICartReader, CartReader>();
        }

        public static void InitializeReadData(
            IEventStoreConnection conn, 
            IRepository<Product> productRepository,
            IRepository<Customer> customerRepository)
        {
            conn.ConnectAsync().Wait();

            if (!productRepository.FindAllAsync(x => true).Result.Any() &&
                !customerRepository.FindAllAsync(x => true).Result.Any())
            {
                SeedReadModel(productRepository, customerRepository);
            }
        }

        private static void SeedReadModel(IRepository<Product> productRepository, IRepository<Customer> customerRepository)
        {
            var insertingProducts = new[] {
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
