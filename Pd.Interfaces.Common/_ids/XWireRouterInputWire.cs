using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Pd.Interfaces.Common;

/// <summary>
///     index drutu wejścia routera
/// </summary>
[CompactSerializer]
[JsonConverter(typeof(XWireRouterInputWireJsonConverter))]
public readonly struct XWireRouterInputWire : IIntegerBasedKey, IEquatable<XWireRouterInputWire>,
    IComparable<XWireRouterInputWire>
{
    public XWireRouterInputWire(int value)
    {
        Value = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XWireRouterInputWire operator +(XWireRouterInputWire left, int right)
    {
        return left.IsEmpty ? left : new XWireRouterInputWire(left.Value + right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XWireRouterInputWire operator --(XWireRouterInputWire src)
    {
        return src.IsEmpty ? src : new XWireRouterInputWire(src.Value - 1);
    }

    public static bool operator ==(XWireRouterInputWire left, XWireRouterInputWire right)
    {
        return left.Value == right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XWireRouterInputWire operator ++(XWireRouterInputWire src)
    {
        return src.IsEmpty ? src : new XWireRouterInputWire(src.Value + 1);
    }

    public static bool operator !=(XWireRouterInputWire left, XWireRouterInputWire right)
    {
        return left.Value != right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XWireRouterInputWire operator -(XWireRouterInputWire left, int right)
    {
        return left.IsEmpty ? left : new XWireRouterInputWire(left.Value - right);
    }

    public XWireIdx AsWireIdx()
    {
        return new XWireIdx(Value);
    }

    public int CompareTo(XWireRouterInputWire other)
    {
        return Value.CompareTo(other.Value);
    }

    public bool Equals(XWireRouterInputWire other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is XWireRouterInputWire s && Value.Equals(s.Value);
    }

    public bool Equals(XWireRouterInputWire? obj)
    {
        return obj is not null && Value.Equals(obj.Value.Value);
    }

    public override int GetHashCode()
    {
        return Value;
    }

    public bool IsZero()
    {
        return Value == 0;
    }

    public override string ToString()
    {
        return "wire router input wire " + Value;
    }

    public string ValueAsText()
    {
        return IsEmpty ? "Empty" : Value.ToString();
    }

    public bool IsEmpty => Value == int.MinValue;

    public static XWireRouterInputWire Empty => new XWireRouterInputWire(int.MinValue);

    public static XWireRouterInputWire Zero => new XWireRouterInputWire(0);

    public int Value { get; }
}
