using System;
using System.ComponentModel;
using System.Globalization;
using Pd.Interfaces.Common;

namespace Pd.Interfaces.Geometry;

public sealed class DiffMinuteAngleTypeConverter : BasicTypeConverter<DiffMinuteAngle>
{
    protected override DiffMinuteAngle ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture,
        string value)
    {
        return DiffMinuteAngle.Parse(value, culture.NumberFormat).GetValueOrThrow();
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture,
        DiffMinuteAngle value, Type destinationType)
    {
        return value.ToString(culture);
    }
}
