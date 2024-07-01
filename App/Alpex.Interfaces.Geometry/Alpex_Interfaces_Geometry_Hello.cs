using System;
using System.Collections.Generic;
using System.Globalization;

namespace Alpex.Interfaces.Geometry;

public sealed class Alpex_Interfaces_Geometry_Hello
{
    public static IEnumerable<(Type, Func<object?, CultureInfo, string?>)> GetValidators()
    {
        yield return (typeof(AngleDeg), AngleDeg.IsValidAngleDegString);
        yield return (typeof(DiffAngleDeg), DiffAngleDeg.IsValidDiffAngleDegString);
        yield return (typeof(DiffMinuteAngle), DiffMinuteAngle.IsValidDiffMinuteAngleString);

        yield return (typeof(MinuteAngle), MinuteAngle.IsValidMinuteAngleString);
        yield return (typeof(Scale), Scale.IsValidScaleString);
    }
}
