using System;

namespace Alpex.Interfaces.Common;

public static class TeePresenationKindExt
{
    public static TeePresenationGroup ToTeePresenationGroup(this TeePresenationKind kind)
    {
        return kind switch
        {
            TeePresenationKind.Rising 
                or TeePresenationKind.Flat 
                or TeePresenationKind.Drooping
                => TeePresenationGroup.LeftRight,
            TeePresenationKind.Parallel
                => TeePresenationGroup.ForwardBackward,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
