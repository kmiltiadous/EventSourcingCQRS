using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Mobile.Client.Annotations;

namespace Mobile.Client.ViewModels
{
    public class ViewModelBase<T> : IViewModel<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void InitializeDataAsync(T data)
        {
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ViewModelBase : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void InitializeDataAsync()
        {}

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
