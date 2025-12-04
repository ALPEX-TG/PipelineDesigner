using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Alpex.Interfaces.Common
{
    public sealed class ObservableCollectionEx
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObservableCollectionEx<T> Make<T>(NotifyCollectionChangedEventHandler? handler = null)
        {
            var result = new ObservableCollectionEx<T>();
            if (handler != null)
                result.CollectionChanged += handler;
            return result;
        }

        /*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObservableCollectionEx<T> Make<T>(NotifyCollectionChangedEventHandler handler = null)
        {
            var collection = new ObservableCollectionEx<T>();
            if (handler != null)
                collection.CollectionChanged += handler;
            return collection;
        }*/

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObservableCollectionEx<T> Make<T>(params T[] elements)
        {
            return new ObservableCollectionEx<T>(elements);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObservableCollectionEx<T> Make<T>(IEnumerable<T> elements)
        {
            return new ObservableCollectionEx<T>(elements);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObservableCollectionEx<T> Make<T>(List<T> elements)
        {
            return new ObservableCollectionEx<T>(elements);
        }
    }

    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        public ObservableCollectionEx()
        {
        }

        public ObservableCollectionEx(List<T> list)
            : base(list)
        {
        }

        public ObservableCollectionEx(IEnumerable<T> collection)
            : base(collection)
        {
        }

        protected override void ClearItems()
        {
            CheckReentrancy();
            var count = Items.Count;
            while (count > 0)
            {
                RemoveAt(count - 1);
                count = Items.Count;
            }

            // wersja jeden - wywołyję parenta
            base.ClearItems();
            // wersja 2 robię ręcznie - nie prawdą jest, że zmienia się Count
            // => this.CheckReentrancy();
            // => base.ClearItems();
            // => this.OnPropertyChanged("Count");

            // => this.OnPropertyChanged("Item[]");
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));

            // => this.OnCollectionReset();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
