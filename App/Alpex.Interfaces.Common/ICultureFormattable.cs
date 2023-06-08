using System;

namespace Alpex.Interfaces.Common;

public interface ICultureFormattable
{
    string ToString(IFormatProvider formatProvider);
}
