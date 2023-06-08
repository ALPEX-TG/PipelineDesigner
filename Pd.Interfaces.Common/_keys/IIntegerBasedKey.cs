using System;

namespace Pd.Interfaces.Common;

public interface IIntegerBasedKey
{
    int Value { get; }
}

public static class IntegerBasedKeyExtensions
{
    public static string ToKeyWithLabel(this IIntegerBasedKey key, string label)
    {
        var v   = key.Value;
        var txt = v == int.MinValue ? "-" : v.ToInvariantString();
        return label + txt;
    }

    public static string ToKeyWithLabel(this IGuidBasedKey key, string label)
    {
        var v   = key.Value;
        var txt = v.Equals(Guid.Empty) ? "-" : v.ToString("N");
        return label + txt;
    }

    public static string ToShortString(this IIntegerBasedKey x, bool addCommaAfter = false)
    {
        var v = x.Value;
        if (v == int.MinValue) return "E";
        if (addCommaAfter)
            return v.ToInvariantString() + ",";
        return v.ToInvariantString();
    }

    public static string ToShortString(this IGuidBasedKey x, bool addCommaAfter = false)
    {
        var v = x.Value;
        if (v.Equals(Guid.Empty)) return "E";
        if (addCommaAfter)
            return v.ToString("N") + ",";
        return v.ToString("N");
    }
}
