using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Client.Extensions
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in list)
            {
                collection.Add(item);
            }

            return collection;
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if(collection == null)
                return;

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
