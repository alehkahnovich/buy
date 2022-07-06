using System;
using System.Threading.Tasks;
using Buy.Upload.IO.Containers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Buy.Upload.IO.Connections
{
    internal sealed class AzureConnectionFactory : IAzureConnectionFactory {
        private readonly IFileStorageConnectionFactory _connectionFactory;

        public AzureConnectionFactory(IFileStorageConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task<CloudBlobContainer> GetContainer(ContainerType container) {
            if (!CloudStorageAccount.TryParse(_connectionFactory.GetConnection(), out var account)) return null;
            var reference = account.CreateCloudBlobClient().GetContainerReference(Enum.GetName(typeof(ContainerType), container)?.ToLowerInvariant());
            await reference.CreateIfNotExistsAsync().ConfigureAwait(false);
            return reference;
        }
    }
}