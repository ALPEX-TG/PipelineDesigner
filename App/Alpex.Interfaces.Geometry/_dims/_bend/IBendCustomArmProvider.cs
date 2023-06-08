namespace Alpex.Interfaces.Geometry;

/// <summary>
///     Podaje długości ramion trójnika jeśli są inne niż wartość domyślna
/// </summary>
public interface IBendCustomArmProvider
{
    void GetCustomArmLengths(out SimpleLength? armLengthA, out SimpleLength? armLengthB);
    SimpleLength GetDefaultArmLength();
}
