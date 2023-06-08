using System;

namespace Pd.Interfaces.Common;

public interface ICultureFormattable
{
    string ToString(IFormatProvider formatProvider);
}
