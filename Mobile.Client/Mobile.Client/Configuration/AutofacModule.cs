using Autofac;
using Mobile.Client.Factories;
using Mobile.Client.Services;
using Xamarin.Forms;

namespace Mobile.Client.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();
            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();
            builder.Register(context =>App.Current.MainPage.Navigation).SingleInstance();
        }
    }
}
