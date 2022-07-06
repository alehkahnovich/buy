using System;
using System.Threading.Tasks;
using Buy.Rasterization.Business.Converters.Abstractions;
using Buy.Rasterization.Business.Resizers;
using Buy.Rasterization.DataAccess.Domain;
using Buy.Rasterization.DataAccess.Repositories;
using Buy.Rasterization.IO.Connections;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Buy.Rasterization.Business.Converters
{
    public sealed class Thumbnail : IThumbnail {
        private const int Size = 350;
        private const string Type = "thumbnail";
        private readonly IRasterizationStorage _storage;
        private readonly IStoreConnection _ioConnection;
        private readonly IArtifactStorage _artifact;
        private readonly IResizer _resizer;
        public Thumbnail(IRasterizationStorage storage, IStoreConnection ioConnection, IResizer resizer, IArtifactStorage artifact) {
            _storage = storage;
            _ioConnection = ioConnection;
            _resizer = resizer;
            _artifact = artifact;
        }

        public async Task RasterizeAsync(int requestId) {
            var request = await _storage.GetAsync(requestId).ConfigureAwait(false);
            if (request == null) return;

            var container = await _ioConnection.GetContainer().ConfigureAwait(false);
            var reference = container.GetBlobReference(request.UploadKey.ToString());

            var resized = await ResizeAsync(reference).ConfigureAwait(false);

            var rawKey = Guid.NewGuid();
            var key = $"{rawKey}";
            var blockReference = container.GetBlockBlobReference(key);

            await UploadAsync(blockReference, resized).ConfigureAwait(false);

            await _artifact.SaveAsync(new Artifact {
                RequestId = requestId,
                Type = Type,
                UploadKey = rawKey,
                SortOrder = 0
            });
        }

        private async Task UploadAsync(ICloudBlob blob, string path) => await blob.UploadFromFileAsync(path).ConfigureAwait(false);

        private async Task<string> ResizeAsync(CloudBlob blob) {
            using (var stream = await blob.OpenReadAsync().ConfigureAwait(false)) {
                return await _resizer.Resize(stream, Size).ConfigureAwait(false);
            }
        }
    }
}