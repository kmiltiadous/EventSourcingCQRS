using Autofac;
using Mobile.Client.Factories;
using Mobile.Client.Models;
using Mobile.Client.ViewModels;
using Mobile.Client.Views;
using Xamarin.Forms;

namespace Mobile.Client.Configuration
{
    public class Bootstrapper : AutofacBootstrapper
    {
        private readonly App application;

        public Bootstrapper(App application)
        {
            this.application = application;
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterModule<ViewModule>();
            builder.RegisterModule<ServiceModule>();
        }

        protected override void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<CartSummaryViewModel, CartSummaryPage>();
            viewFactory.Register<CartViewModel, Cart, CartPage>();
            viewFactory.Register<CartItemSelectionViewModel, Cart, CartItemSelectionPage>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            var viewFactory = container.Resolve<IViewFactory>();
            var mainPage = viewFactory.Resolve(out CartSummaryViewModel viewModel);
            viewModel.InitializeDataAsync();
            var navigationPage = new NavigationPage(mainPage)
            {
                BarBackgroundColor = Color.LightSteelBlue
            };

            application.MainPage = navigationPage;
        }
    }
}
