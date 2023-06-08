using System;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Common;

/// <summary>
///     UID typu danych w blobie
/// </summary>
[CompactSerializer]
[JsonConverter(typeof(XDataTypeUidJsonConverter))]
public readonly struct XDataTypeUid : IGuidBasedKey, IEquatable<XDataTypeUid>
{
    public XDataTypeUid(Guid value)
    {
        Value = value;
    }

    public static XDataTypeUid NewId()
    {
        return new XDataTypeUid(Guid.NewGuid());
    }

    public static bool operator ==(XDataTypeUid left, XDataTypeUid right)
    {
        return left.Value == right.Value;
    }

    public static bool operator !=(XDataTypeUid left, XDataTypeUid right)
    {
        return left.Value != right.Value;
    }

    public static XDataTypeUid Parse(string guid)
    {
        return new XDataTypeUid(Guid.Parse(guid));
    }

    public bool Equals(XDataTypeUid other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is XDataTypeUid s && Value.Equals(s.Value);
    }

    public bool Equals(XDataTypeUid? obj)
    {
        return obj is not null && Value.Equals(obj.Value.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return "Data type UID " + Value;
    }

    public string ValueAsText()
    {
        return IsEmpty ? "Empty" : Value.ToString();
    }

    public bool IsEmpty => Value.Equals(Guid.Empty);

    public static XDataTypeUid Empty => new XDataTypeUid(Guid.Empty);

    public Guid Value { get; }
}
