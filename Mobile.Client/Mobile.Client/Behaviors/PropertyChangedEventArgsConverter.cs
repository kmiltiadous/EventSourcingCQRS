using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace Mobile.Client.Behaviors
{
    public class PropertyChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new TargetPropertyChanged(parameter, ((PropertyChangedEventArgs)value).PropertyName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
