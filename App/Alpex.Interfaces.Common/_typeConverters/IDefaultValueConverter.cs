﻿using System;
using System.Globalization;

namespace Alpex.Interfaces.Common;

public interface IDefaultValueConverter
{
    object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

    bool CanConvertBack { get; }
}
