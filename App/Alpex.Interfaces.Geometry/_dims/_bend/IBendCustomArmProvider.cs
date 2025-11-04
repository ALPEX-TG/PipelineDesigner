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
        var d = provider.GetArmLengthInfo().Default;
        var ab = provider.GetArms();
        return ab.ToArmLengths(d);
    }
}


public record struct BendArmLengthRange(
    SimpleLength Min,
    SimpleLength Max,
    SimpleLength Default);

public record struct DeclaredBendArmlLengths(SimpleLength? LengthA, SimpleLength? LengthB)
{
    public static implicit operator DeclaredBendArmlLengths(ArmLengths src)
        => new DeclaredBendArmlLengths(src.LengthA, src.LengthB);

    public ArmLengths ToArmLengths(SimpleLength defaultValue)
        => new ArmLengths(LengthA ?? defaultValue, LengthB ?? defaultValue);
}

public record struct ArmLengths(SimpleLength LengthA, SimpleLength LengthB);    