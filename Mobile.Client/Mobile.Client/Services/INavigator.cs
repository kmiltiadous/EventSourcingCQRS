using System.Threading.Tasks;
using Mobile.Client.ViewModels;

namespace Mobile.Client.Services
{
    public interface INavigator
    {
        Task PushAsync<TViewModel>()
            where TViewModel : class, IViewModel;

        Task PushAsync<TViewModel, TData>(TData data)
            where TViewModel : class, IViewModel<TData>;
    }
}
