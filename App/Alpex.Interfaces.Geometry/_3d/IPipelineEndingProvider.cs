#if WPF
#else
#endif

namespace Alpex.Interfaces.Geometry
{
    public interface IPipelineEndingProvider
    {
        PipelineEndingInfo GetEndingInfo(int endingIndex);

        /// <summary>
        ///     Ilość zakończeń kształtki, np.2 dla rury i kolana, 3 dla trójnika
        /// </summary>
        int GetEndingsCount();
    }
}
