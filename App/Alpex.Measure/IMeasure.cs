using System;
using Alpex.Interfaces.Common;

namespace Alpex.Measure;

public interface IMeasure
{
    XDataTypeUid GetDataTypeUid();
}

public interface IMeasureWithDate : IMeasure
{
    DateTimeOffset? GetDate();
}
