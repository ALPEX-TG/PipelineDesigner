namespace Alpex.Interfaces.Geometry.Test;

public sealed class LengthTests
{
    [Fact]
    public void T01_Should_Convert_ToUnitIfPossible()
    {
        var l = new Length(1, "m");
        var q = l.ToUnitIfPossible("mm");
        Assert.Equal(1000, q.Value);
        Assert.Equal("mm", q.Unit);
    }
}
