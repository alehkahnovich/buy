using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Buy.Rasterization.IO.Connections
{
    internal sealed class StoreConnection : IStoreConnection {
        private const string ConfigurationKey = "BlobStorageConnectionString";
        private readonly string _connectionString;

        public StoreConnection(IConfiguration configuration) {
            _connectionString = configuration.GetSection(ConfigurationKey).Value;
        }

        public async Task<CloudBlobContainer> GetContainer() {
            if (!CloudStorageAccount.TryParse(_connectionString, out var account)) return null;
            var reference = account.CreateCloudBlobClient().GetContainerReference(Enum.GetName(typeof(ContainerType), ContainerType.Rasterization)?.ToLowerInvariant());
            await reference.CreateIfNotExistsAsync().ConfigureAwait(false);
            return reference;
        }
    }
}