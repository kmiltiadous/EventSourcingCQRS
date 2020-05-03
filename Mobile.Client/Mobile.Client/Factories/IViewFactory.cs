using Mobile.Client.ViewModels;
using Xamarin.Forms;

namespace Mobile.Client.Factories
{
    public interface IViewFactory
    {
        void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : Page;

        void Register<TViewModel, TData, TView>()
            where TViewModel : class, IViewModel<TData>
            where TView : Page;

        Page Resolve<TViewModel>()
            where TViewModel : class, IViewModel;

        Page Resolve<TViewModel>(out TViewModel viewModel)
            where TViewModel : class, IViewModel;

        Page Resolve<TViewModel, TData>(out TViewModel viewModel)
            where TViewModel : class, IViewModel<TData>;
    }
}
