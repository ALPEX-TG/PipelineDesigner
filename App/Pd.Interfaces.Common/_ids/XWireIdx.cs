using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Pd.Interfaces.Common;

/// <summary>
///     Indeks drutu w rurze, liczony w porządku fasady bo w rurze mogą być poprzestawiane
/// </summary>
[CompactSerializer]
[JsonConverter(typeof(XWireIdxJsonConverter))]
public readonly struct XWireIdx : IIntegerBasedKey, IEquatable<XWireIdx>, IComparable<XWireIdx>
{
    public XWireIdx(int value)
    {
        Value = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XWireIdx operator +(XWireIdx left, int right)
    {
        return left.IsEmpty ? left : new XWireIdx(left.Value + right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XWireIdx operator --(XWireIdx src)
    {
        return src.IsEmpty ? src : new XWireIdx(src.Value - 1);
    }

    public static bool operator ==(XWireIdx left, XWireIdx right)
    {
        return left.Value == right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XWireIdx operator ++(XWireIdx src)
    {
        return src.IsEmpty ? src : new XWireIdx(src.Value + 1);
    }

    public static bool operator !=(XWireIdx left, XWireIdx right)
    {
        return left.Value != right.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XWireIdx operator -(XWireIdx left, int right)
    {
        return left.IsEmpty ? left : new XWireIdx(left.Value - right);
    }

    public int CompareTo(XWireIdx other)
    {
        return Value.CompareTo(other.Value);
    }

    public bool Equals(XWireIdx other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is XWireIdx s && Value.Equals(s.Value);
    }

    public bool Equals(XWireIdx? obj)
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
        return "wire in pipe F-index " + Value;
    }

    public string ValueAsText()
    {
        return IsEmpty ? "Empty" : Value.ToString();
    }

    public bool IsEmpty => Value == int.MinValue;

    public static XWireIdx Empty => new XWireIdx(int.MinValue);

    public static XWireIdx Zero => new XWireIdx(0);

    public int Value { get; }
}
