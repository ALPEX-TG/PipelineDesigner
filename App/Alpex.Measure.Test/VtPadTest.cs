using Alpex.Interfaces.Common;
using Newtonsoft.Json;

namespace Alpex.Measure.Test;

public class VtPadTest
{
    [Fact]
    public void T01_ShouldSerialize()
    {
        var          pad      = new VtPad(XAlarmContainerUid.Parse("1BBF7E09-3AE3-41C1-9738-0583D3E15010"), 3, new XWireRouterInputWire(4));
        var          json     = JsonConvert.SerializeObject(pad);
        const string expected = @"{""ComponentUid"":""1bbf7e093ae341c197380583d3e15010"",""EndIndex"":3,""WireIndex"":4}";
        Assert.Equal(expected, json);
    }

    [Fact]
    public void T02_ShouldDeserialize()
    {
        const string json     = @"{""ComponentUid"":""1bbf7e093ae341c197380583d3e15010"",""EndIndex"":3,""WireIndex"":4}";
        var          got      = JsonConvert.DeserializeObject<VtPad>(json);
        var          expected = new VtPad(XAlarmContainerUid.Parse("1BBF7E09-3AE3-41C1-9738-0583D3E15010"), 3, new XWireRouterInputWire(4));
        Assert.Equal(expected, got);
    }
}
