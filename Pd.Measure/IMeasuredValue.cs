namespace Pd.Measure;

public interface IMeasuredValue
{
    decimal       Value  { get; }
    MeasureUnit?  Unit   { get; }
    MeasureStatus Status { get; }
}
