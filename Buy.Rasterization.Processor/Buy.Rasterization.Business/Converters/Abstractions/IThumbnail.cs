using System.Threading.Tasks;

namespace Buy.Rasterization.Business.Converters.Abstractions
{
    public interface IThumbnail {
        Task RasterizeAsync(int requestId);
    }
}