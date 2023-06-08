using System.Collections.Generic;
using Pd.Interfaces.Common;

namespace Pd.Measure;

public class AbstractImpulsAlarmMeasure<T>
{
    public AbstractImpulsAlarmMeasure(TDate date, string location, IReadOnlyList<T> items)
    {
        Date     = date;
        Location = location;
        Items    = items;
    }

    public TDate Date { get; }

    public string Location { get; }

    public IReadOnlyList<T> Items { get; }
}
