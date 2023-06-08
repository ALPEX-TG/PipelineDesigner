namespace Pd.Interfaces.Common;

public interface IBuilder<out TResult>
{
    TResult Build();
}
