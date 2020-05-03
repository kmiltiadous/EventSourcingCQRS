using Autofac;
using Mobile.Client.Behaviors;
using Mobile.Client.Services;

namespace Mobile.Client.Configuration
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartService>().As<ICartService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<GenericService>().As<IGenericService>();
            builder.RegisterType<AnimateBehavior>().As<IAnimate>();
        }
    }
}
