namespace Alpex.Interfaces.Geometry;

public interface IPipeDimensionsMm : IInsulatedSteelPipeDimensionsMm
{
    double DefaultBendRadius { get; }
    double Margin            { get; }

    /// <summary>
    ///     Grubość plastikowej osłony na zewnątrz pianki
    /// </summary>
    double PlasticCoveringThickness { get; }

    double SteelThickness { get; }
}
