using System;
using System.IO;
using System.Threading.Tasks;
using Buy.Upload.IO.Connections;
using Buy.Upload.IO.Containers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Buy.Upload.IO.Storage
{
    internal sealed class AzureFileStorage : IFileStorage {
        private readonly IAzureConnectionFactory _connectionFactory;
        public AzureFileStorage(IAzureConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public async Task<Stream> GetAsync(string key) {
            var container = await GetContainer().ConfigureAwait(false);
            var reference = container.GetBlobReference(key);
            if (!await reference.ExistsAsync().ConfigureAwait(false)) return null;
            return await reference.OpenReadAsync().ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(string key) {
            var container = await GetContainer().ConfigureAwait(false);
            return await container.GetBlockBlobReference(key).DeleteIfExistsAsync().ConfigureAwait(false);
        }

        public async Task<string> UploadAsync(Stream stream) {
            var container = await GetContainer().ConfigureAwait(false);
            var key = $"{Guid.NewGuid()}";
            var reference = container.GetBlockBlobReference(key);
            await reference.UploadFromStreamAsync(stream).ConfigureAwait(false);
            return key;
        }

        private async Task<CloudBlobContainer> GetContainer() =>
            await _connectionFactory.GetContainer(ContainerType.Rasterization).ConfigureAwait(false);
    }
}