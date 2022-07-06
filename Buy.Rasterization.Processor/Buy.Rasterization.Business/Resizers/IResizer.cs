using System.IO;
using System.Threading.Tasks;

namespace Buy.Rasterization.Business.Resizers
{
    public interface IResizer {
        Task<string> Resize(Stream stream, int size);
    }
}