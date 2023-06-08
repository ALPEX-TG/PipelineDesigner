using JetBrains.Annotations;

namespace Alpex.Interfaces.Common;

public interface IOrderedValue<out T>
{
    T GetValue();

    int Order { get; }
}

public readonly struct SimpleColumn : IOrderedValue<string>
{
    public SimpleColumn(string header, int order)
    {
        Header = header;
        Order  = order;
    }

    public static SimpleColumn Make([CanBeNull] IOrderedValue<string> value, int defaultOrder)
    {
        switch (value)
        {
            case null:
                return new SimpleColumn("", defaultOrder);
            case SimpleColumn sc:
                return sc;
            default:
                return new SimpleColumn(value.GetValue(), value.Order);
        }
    }

    public string GetValue()
    {
        return Header;
    }

    public override string ToString()
    {
        return $"{Header} {Order}";
    }

    public string Header { get; }

    public int Order { get; }
}
