using Alpex.Interfaces.Common;

namespace Alpex.Measure;

public sealed partial class ReportImpulsAlarmMeasure
{
    public sealed class MeasureItem : AbstractImpulsAlarmMeasureItem
    {
        public MeasureItem(ImpulsAlarmMeasureKind type, string loopName, SimpleMeasureValue copperWire,
            SimpleMeasureValue soldedWire, SimpleMeasureValue measureVoltage, string comments,
            SimpleColumn pipe,
            bool loopNameIsAutomatic)
            : base(type, loopName, copperWire, soldedWire, measureVoltage, comments,
                loopNameIsAutomatic)
        {
            Pipe = pipe;
        }

        public SimpleColumn Pipe { get; }
    }
}
