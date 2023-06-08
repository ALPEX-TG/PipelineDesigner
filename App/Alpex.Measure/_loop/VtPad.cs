using System;
using Alpex.Interfaces.Common;

namespace Alpex.Measure;

// [Auto.ShouldSerializeInfo("!{0}.IsEmpty()")]
public readonly struct VtPad : IEquatable<VtPad>
{
    public VtPad(XAlarmContainerUid componentUid, int endIndex, XWireRouterInputWire wireIndex)
    {
        ComponentUid = componentUid;
        EndIndex     = endIndex;
        WireIndex    = wireIndex;
    }

    public VtPad(XAlarmContainerUid uid, TeeConnectorEndPlusWire teeConnectorEnd)
    {
        ComponentUid = uid;
        EndIndex     = (int)teeConnectorEnd.End;
        WireIndex    = teeConnectorEnd.WireIdx;
    }

    public static bool operator ==(VtPad left, VtPad right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(VtPad left, VtPad right)
    {
        return !left.Equals(right);
    }

    public bool Equals(VtPad other)
    {
        return ComponentUid.Equals(other.ComponentUid)
               && EndIndex == other.EndIndex
               && WireIndex == other.WireIndex;
    }

    public override bool Equals(object? obj)
    {
        return obj is VtPad other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = ComponentUid.Value.GetHashCode();
            hashCode = (hashCode * 397) ^ EndIndex;
            hashCode = (hashCode * 397) ^ WireIndex.Value;
            return hashCode;
        }
    }

    public bool IsEmpty()
    {
        return ComponentUid.IsEmpty;
    }

    public override string ToString()
    {
        return $"e={EndIndex}, w={WireIndex.Value} of {ComponentUid.Value:D}";
    }

    public        XAlarmContainerUid   ComponentUid { get; }
    public        int                  EndIndex     { get; }
    public        XWireRouterInputWire WireIndex    { get; }
    public static VtPad                Empty        { get; } = new VtPad();
}
