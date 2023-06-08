using System;
using System.ComponentModel;
using System.Globalization;
using Pd.Interfaces.Common;

namespace Pd.Interfaces.Geometry;

public sealed class LengthTypeConverter : BasicTypeConverter<Length>
{
    protected override Length ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture, string value)
    {
        culture = culture ?? CultureInfo.InvariantCulture;
        return Length.Parse(value, culture.NumberFormat).GetValueOrThrow();
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture, Length value,
        Type destinationType)
    {
        return value.ToString(culture);
    }
}
