using System.ComponentModel;

namespace Mobile.Client.ViewModels
{
    public interface IViewModel<in T> : INotifyPropertyChanged
    {
        void InitializeDataAsync(T data);
    }

    public interface IViewModel : INotifyPropertyChanged
    {
        void InitializeDataAsync();
    }
}