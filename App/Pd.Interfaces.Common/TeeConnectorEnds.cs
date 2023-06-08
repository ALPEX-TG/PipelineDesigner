namespace Pd.Interfaces.Common;

public enum TeeConnectorEnds : byte
{
    End1 = 0,
    End2 = 1,
    Fork = 2
}

public enum TeeNodeEnds : byte
{
    End1ToSupply = 0,
    End2ToReceiver = 1,
    ForkToReceiver = 2
}
