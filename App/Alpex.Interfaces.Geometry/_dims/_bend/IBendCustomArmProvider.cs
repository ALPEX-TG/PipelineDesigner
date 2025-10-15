namespace Alpex.Interfaces.Geometry;

/// <summary>
///     Podaje długości ramion trójnika jeśli są inne niż wartość domyślna
/// </summary>
public interface IBendCustomArmProvider
{
    DeclaredBendArmlLengths GetArms();
    BendArmLengthRange GetArmLengthInfo();
}

public static class BendCustomArmProviderExtensions
{
    public static ArmLengths GetArmLengths(this IBendCustomArmProvider provider)
    {
        var (a, b) = provider.GetArms();
        if (a.HasValue && b.HasValue)
            return new ArmLengths(a.Value, b.Value);
        var info = provider.GetArmLengthInfo();
        var aa   = a ?? info.Default;
        var bb   = b ?? info.Default;
        return new ArmLengths(aa, bb);
    }
}


public record struct BendArmLengthRange(
    SimpleLength Min,
    SimpleLength Max,
    SimpleLength Default);

public record struct DeclaredBendArmlLengths(SimpleLength? LengthA, SimpleLength? LengthB);    
public record struct ArmLengths(SimpleLength LengthA, SimpleLength LengthB);    