using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Buy.Kafka.Consumers;
using Buy.Rasterization.Contract.Messages;
using Buy.Upload.Business.Contracts;
using Buy.Upload.Business.Converters;
using Buy.Upload.Business.Services.Abstractions;
using Buy.Upload.Business.Validation.Abstractions;
using Buy.Upload.Contracts.Uploads;
using Buy.Upload.DataAccess.Repositories.Abstractions;
using Buy.Upload.IO.Storage;

namespace Buy.Upload.Business.Services
{
    public sealed class UploadService : IUploadService {
        private readonly IUploadRequestStorage _storage;
        private readonly IUploadRequestValidator _validator;
        private readonly IFileStorage _blobStorage;
        private readonly IProducer _producer;

        public UploadService(IUploadRequestStorage storage, IFileStorage blobStorage, IUploadRequestValidator validator, IProducer producer) {
            _storage = storage;
            _blobStorage = blobStorage;
            _validator = validator;
            _producer = producer;
        }

        public async Task<UploadedRequest> UploadAsync(UploadRequest request) {
            if (!await _validator.ValidateAsync(request).ConfigureAwait(false))
                throw new ArgumentException("Request is not valid", nameof(request));

            var uploadKey = await _blobStorage.UploadAsync(request.Stream).ConfigureAwait(false);
            var result = await _storage.SaveAsync(new DataAccess.Domains.Request {
                RawName = request.FileName,
                UploadKey = Guid.Parse(uploadKey)
            }).ConfigureAwait(false);

            await _producer.ProduceAsync("rasterization",
                    result.RequestId,
                    new RasterizationRequest { RequestId = result.RequestId })
            .ConfigureAwait(false);

            return UploadRequestConvert.Convert(result);
        }

        public async Task<UploadRequest> GetAsync(int key) {
            var request = await _storage.GetAsync(key).ConfigureAwait(false);
            if (request == null) return null;
            return new UploadRequest(await _blobStorage.GetAsync(request.UploadKey.ToString()).ConfigureAwait(false), MediaTypeNames.Application.Octet) {
                FileName = request.RawName,
                Key = request.UploadKey.ToString()
            };
        }
    }
}