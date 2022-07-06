using System;
using System.Threading.Tasks;
using Buy.Upload.Business.Contracts;
using Buy.Upload.Business.Services.Abstractions;
using Buy.Upload.DataAccess.Repositories.Abstractions;
using Buy.Upload.IO.Storage;

namespace Buy.Upload.Business.Services
{
    public sealed class ArtifactService : IArtifactService {
        private readonly IArtifactStorage _artifactStorage;
        private readonly IUploadRequestStorage _requestStorage;
        private readonly IFileStorage _blobStorage;
        public ArtifactService(IArtifactStorage artifactStorage, IFileStorage blobStorage, IUploadRequestStorage requestStorage) {
            _artifactStorage = artifactStorage;
            _blobStorage = blobStorage;
            _requestStorage = requestStorage;
        }

        public async Task<ArtifactStream> GetStreamAsync(string type, int requestId) {
            var artifactType = ResolveArtifactType(type);

            if (!artifactType.HasValue)
                throw new ArgumentException("Invalid artifact type", nameof(type));

            if (!await _requestStorage.ExistsAsync(requestId).ConfigureAwait(false))
                throw new ArgumentException("Invalid request identity", nameof(requestId));

            var artifact = await _artifactStorage.GetAsync(type.ToLowerInvariant(), requestId).ConfigureAwait(false);

            if (artifact == null)
                return null;

            return new ArtifactStream {
                Id = artifact.ArtifactId,
                ContentType = ResolveContentType(artifactType.Value),
                Stream = await _blobStorage
                    .GetAsync(artifact.UploadKey.ToString())
                    .ConfigureAwait(false)
            };
        }

        public async Task<ArtifactStream> GetArtifactAsync(int key) {
            var artifact = await _artifactStorage.GetAsync(key).ConfigureAwait(false);

            if (artifact == null)
                return null;

            var type = ResolveArtifactType(artifact.Type) ?? ArtifactType.Thumbnail;

            return new ArtifactStream {
                Id = artifact.ArtifactId,
                ContentType = ResolveContentType(type),
                Stream = await _blobStorage
                    .GetAsync(artifact.UploadKey.ToString())
                    .ConfigureAwait(false)
            };
        }

        private static ArtifactType? ResolveArtifactType(string type) {
            if (string.IsNullOrEmpty(type))
                return null;

            foreach (ArtifactType name in Enum.GetValues(typeof(ArtifactType))) {
                if (string.Equals(type, Enum.GetName(typeof(ArtifactType), name), StringComparison.OrdinalIgnoreCase))
                    return name;
            }

            return null;
        }

        private static string ResolveContentType(ArtifactType artifactType) {
            switch (artifactType) {
                case ArtifactType.Thumbnail:
                    return System.Net.Mime.MediaTypeNames.Image.Jpeg;
                case ArtifactType.Preview:
                    return System.Net.Mime.MediaTypeNames.Image.Jpeg;
                default:
                    return System.Net.Mime.MediaTypeNames.Application.Octet;
            }
        }
    }
}