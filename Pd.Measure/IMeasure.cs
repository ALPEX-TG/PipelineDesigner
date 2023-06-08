using System;
using Pd.Interfaces.Common;

namespace Pd.Measure;

public interface IMeasure
{
    XDataTypeUid GetDataTypeUid();
}

public interface IMeasureWithDate : IMeasure
{
    DateTimeOffset? GetDate();
}
