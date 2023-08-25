namespace Alpex.Interfaces.Geometry.Test;

public class AnglesTest
{
    [Fact]
    public void T01_Should_convert_to_radians()
    {
        var angleDeg = new AngleDeg(90);
        var radians  = (Radians)angleDeg;
        Assert.Equal(Radians.Deg90, radians);
    }

    [Fact]
    public void T02_Should_convert_to_degrees()
    {
        var radians  = Radians.Deg90;
        var angleDeg = (AngleDeg)radians;
        Assert.Equal(90, angleDeg.Degrees);
    }

    [Theory]
    [InlineData(90, 0, 0)]
    [InlineData(0, 15, 3)]
    [InlineData(-90, 30, 6)]
    [InlineData(180, 45, 9)]
    public void T03_Should_convert_to_minutes(int degrees, int minutes, int hours)
    {
        var angleDeg    = new AngleDeg(degrees);
        var minuteAngle = (MinuteAngle)angleDeg;
        Assert.Equal(minutes, minuteAngle.Minutes);
        Assert.Equal(hours, minuteAngle.RoundedHour);
    }
}
