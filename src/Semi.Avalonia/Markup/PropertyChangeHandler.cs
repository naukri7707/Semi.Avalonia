using System;
using Avalonia;

namespace Semi.Avalonia.Markups;

internal class PropertyChangeHandler<TSender, TValue> : IObserver<AvaloniaPropertyChangedEventArgs<TValue>>
    where TSender : AvaloniaObject
{
    public PropertyChangeHandler(Action<TSender, TValue> onPropertyChanged)
    {
        _onPropertyChanged = onPropertyChanged;
    }

    private readonly Action<TSender, TValue> _onPropertyChanged;

    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(AvaloniaPropertyChangedEventArgs<TValue> value)
    {
        if (value is { Sender: TSender sender, NewValue.HasValue: true })
        {
            _onPropertyChanged(sender, value.NewValue.Value);
        }
    }
}