namespace Pd.Interfaces.Geometry;

// =================Wymiary w metrach

public interface IPipeDimensions : IInsulatedSteelPipeDimensions
{
    double DefaultBendRadius { get; }
    double NotCoveredMargin  { get; }

    /// <summary>
    ///     Grubość plastikowej osłony na zewnątrz pianki
    /// </summary>
    double PlasticCoveringThickness { get; }

    double SteelThickness { get; }
}
