using System.IO;
using System.Threading.Tasks;

namespace Buy.Upload.IO.Storage
{
    public interface IFileStorage {
        Task<string> UploadAsync(Stream stream);
        Task<Stream> GetAsync(string key);
        Task<bool> DeleteAsync(string key);
    }
}