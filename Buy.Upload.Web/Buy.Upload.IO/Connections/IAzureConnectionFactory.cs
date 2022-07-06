using System.Threading.Tasks;
using Buy.Upload.IO.Containers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Buy.Upload.IO.Connections
{
    internal interface IAzureConnectionFactory {
        Task<CloudBlobContainer> GetContainer(ContainerType container);
    }
}