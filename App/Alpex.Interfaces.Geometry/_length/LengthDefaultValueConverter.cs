using System;
using System.Globalization;
using Alpex.Interfaces.Common;

namespace Alpex.Interfaces.Geometry;

public sealed class LengthDefaultValueConverter : IDefaultValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType == typeof(string) && value is Length)
            return ((Length)value).ToString();
        throw new NotSupportedException($"{value?.GetType().ToString() ?? "NULL"} and {targetType}");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Developer.Nop();
        throw new NotImplementedException();
    }

    public bool CanConvertBack => true;
}
