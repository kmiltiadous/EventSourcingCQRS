using Autofac;
using Mobile.Client.ViewModels;
using Mobile.Client.Views;

namespace Mobile.Client.Configuration
{
    public class ViewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartSummaryViewModel>().SingleInstance();
            builder.RegisterType<CartItemSelectionViewModel>().SingleInstance();
            builder.RegisterType<CartViewModel>().SingleInstance();
            builder.RegisterType<CartSummaryPage>();
            builder.RegisterType<CartPage>();
            builder.RegisterType<CartItemSelectionPage>();
        }
    }
}
