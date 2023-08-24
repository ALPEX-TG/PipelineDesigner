namespace Alpex.Interfaces.Geometry;

public interface IBendDimensionsMm : IPipeDimensionsMm, IBendingAngleProvider
{
    /// <summary>
    ///     Długość ramienia A w mm
    /// </summary>
    double ArmLengthA { get; }

    /// <summary>
    ///     Długość ramienia B w mm
    /// </summary>
    double ArmLengthB { get; }
}

public static class BendDimensionsMmExtensions
{
    public static SimpleLength GetArmLengthA(this IBendDimensionsMm dims)
    {
        return SimpleLength.Mm(dims?.ArmLengthA ?? 0);
    }

    public static SimpleLength GetArmLengthB(this IBendDimensionsMm dims)
    {
        return SimpleLength.Mm(dims?.ArmLengthB ?? 0);
    }
}
