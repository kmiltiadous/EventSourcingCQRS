using System;
using System.Threading.Tasks;
using Mobile.Client.Factories;
using Mobile.Client.ViewModels;
using Xamarin.Forms;

namespace Mobile.Client.Services
{
    public class Navigator : INavigator
    {
        private readonly Lazy<INavigation> navigation;
        private readonly IViewFactory viewFactory;

        private INavigation Navigation => navigation.Value;

        public Navigator(Lazy<INavigation> navigation, IViewFactory viewFactory)
        {
            this.navigation = navigation;
            this.viewFactory = viewFactory;
        }

        public async Task PushAsync<TViewModel>() where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve(out TViewModel viewModel);
            await Navigation.PushAsync(view);
            viewModel.InitializeDataAsync();
        }

        public async Task PushAsync<TViewModel, TData>(TData data) where TViewModel : class, IViewModel<TData>
        {
            var view = viewFactory.Resolve<TViewModel, TData>(out TViewModel viewModel);
            await Navigation.PushAsync(view);
            viewModel.InitializeDataAsync(data);
        }
    }
}
