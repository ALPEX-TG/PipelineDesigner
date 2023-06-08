using Pd.Interfaces.Common;

namespace Pd.Measure;

public class WellKnownCloudDataGroups
{
    public static readonly XDataTypeUid DataPointsMeasureLoopResistance =
        XDataTypeUid.Parse("{60398145-96BA-43CD-B441-7B09A5C27C6A}");

    public static readonly XDataTypeUid DataPointsReflectometricMeasure =
        XDataTypeUid.Parse("{4F2E2869-2987-44AB-AF7F-8B997548B6D2}");
}
