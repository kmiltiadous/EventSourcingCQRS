using System;
using EventSourcingCQRS.Application.Handlers;
using EventSourcingCQRS.Application.PubSub;
using EventSourcingCQRS.Application.Services;
using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.PubSub;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcingCQRS.Application.Configuration
{
    public class EventHandlersModule : IConfigureModule
    {
        private readonly IServiceCollection services;
        public EventHandlersModule(IServiceCollection services)
        {
            this.services = services;
        }

        public void Configure()
        {
            services.AddTransient<ITransientDomainEventPublisher>(s => TransientDomainEventPubSubFactory.CreateInstance(s));
            services.AddTransient<ITransientDomainEventSubscriber, TransientDomainEventPubSub>();
            services.AddTransient<IDomainEventHandler<CartId, CartCreatedEvent>, CartUpdater>();
            services.AddTransient<IDomainEventHandler<CartId, ProductAddedEvent>, CartUpdater>();
            services.AddTransient<IDomainEventHandler<CartId, ProductQuantityChangedEvent>, CartUpdater>();
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
    }
}
