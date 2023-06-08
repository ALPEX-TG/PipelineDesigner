using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Alpex.Interfaces.Common;

public class ObservableCollectionExOfNotifyPropertyChanged<T>
    : ObservableCollectionEx<T>
    where T : INotifyPropertyChanged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AttachEvent(T item)
    {
        if (item is null)
            return;
        item.PropertyChanged += PcOnPropertyChanged;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DettachEvent(T item)
    {
        if (item is null)
            return;
        item.PropertyChanged -= PcOnPropertyChanged;
    }

    protected override void InsertItem(int index, T item)
    {
        CheckReentrancy();
        AttachEvent(item);
        base.InsertItem(index, item);
    }

    protected virtual void OnItemPropertyChanged(T sender, PropertyChangedEventArgs e)
    {
        var handler = ItemPropertyChanged;
        if (handler is null)
            return;
        var args = new ItemPropertyChangedEventArgs(sender, e.PropertyName);
        handler(this, args);
    }


    private void PcOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        OnItemPropertyChanged((T)sender, e);
    }

    protected override void RemoveItem(int index)
    {
        CheckReentrancy();
        DettachEvent(this[index]);
        base.RemoveItem(index);
    }

    protected override void SetItem(int index, T item)
    {
        CheckReentrancy();
        DettachEvent(this[index]);
        AttachEvent(item);
        base.SetItem(index, item);
    }

    public event EventHandler<ItemPropertyChangedEventArgs> ItemPropertyChanged;

    public sealed class ItemPropertyChangedEventArgs : EventArgs
    {
        public ItemPropertyChangedEventArgs(T item, string propertyName)
        {
            Item         = item;
            PropertyName = propertyName;
        }

        public T      Item         { get; }
        public string PropertyName { get; }
    }
}
