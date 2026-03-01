using System.ComponentModel;

namespace Alpex.Interfaces.Common;

public enum PipeFlowDirections
{
    [Description("Nieznany")] Unknown = 0,
    [Description("Zasilanie")] Supply = 1,
    [Description("Powrót")] Return = 2
}
