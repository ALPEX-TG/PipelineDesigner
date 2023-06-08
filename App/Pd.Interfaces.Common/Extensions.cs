using System;
using System.Globalization;

namespace Pd.Interfaces.Common;

public static class Extensions
{
    public static XAlarmContainerUid AsAlarmContainerUid(this Guid x)
    {
        return new XAlarmContainerUid(x);
    }

    public static XAlarmLoopUid AsAlarmLoopUid(this Guid x)
    {
        return new XAlarmLoopUid(x);
    }

    public static XDataTypeUid AsDataTypeUid(this Guid x)
    {
        return new XDataTypeUid(x);
    }

    public static XWireRouterInputWire AsWireRouterInputWire(this int x)
    {
        return new XWireRouterInputWire(x);
    }


    internal static string ToInvariantString(this int x)
    {
        return x.ToString(CultureInfo.InvariantCulture);
    }
}
