using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Buy.Rasterization.IO.Connections
{
    public interface IStoreConnection {
        Task<CloudBlobContainer> GetContainer();
    }
}