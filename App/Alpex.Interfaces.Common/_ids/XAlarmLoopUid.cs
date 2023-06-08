using System;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Common;

/// <summary>
///     Alarm loop UID
/// </summary>
[CompactSerializer]
[JsonConverter(typeof(XAlarmLoopUidJsonConverter))]
public readonly struct XAlarmLoopUid : IGuidBasedKey, IEquatable<XAlarmLoopUid>
{
    public XAlarmLoopUid(Guid value)
    {
        Value = value;
    }

    public static XAlarmLoopUid NewId()
    {
        return new XAlarmLoopUid(Guid.NewGuid());
    }

    public static bool operator ==(XAlarmLoopUid left, XAlarmLoopUid right)
    {
        return left.Value == right.Value;
    }

    public static bool operator !=(XAlarmLoopUid left, XAlarmLoopUid right)
    {
        return left.Value != right.Value;
    }

    public static XAlarmLoopUid Parse(string guid)
    {
        return new XAlarmLoopUid(Guid.Parse(guid));
    }

    public bool Equals(XAlarmLoopUid other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is XAlarmLoopUid s && Value.Equals(s.Value);
    }

    public bool Equals(XAlarmLoopUid? obj)
    {
        return obj is not null && Value.Equals(obj.Value.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return "alarm UID " + Value;
    }

    public string ValueAsText()
    {
        return IsEmpty ? "Empty" : Value.ToString();
    }

    public bool IsEmpty => Value.Equals(Guid.Empty);

    public static XAlarmLoopUid Empty => new XAlarmLoopUid(Guid.Empty);

    public Guid Value { get; }
}
