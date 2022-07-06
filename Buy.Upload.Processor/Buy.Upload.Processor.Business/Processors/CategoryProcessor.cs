using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Upload.IO.Storage;
using Buy.Upload.Processor.Business.Merge.Abstractions;
using Buy.Upload.Processor.Business.Parsers.Abstractions;
using Buy.Upload.Processor.Business.Processors.Abstractions;
using Buy.Upload.Processor.Business.Representation.SourceData;
using Buy.Upload.Processor.DataAccess.Domain;
using Buy.Upload.Processor.DataAccess.Repositories.Abstractions;
using Buy.Upload.Processor.Messages.Abstractions;

namespace Buy.Upload.Processor.Business.Processors
{
    public class CategoryProcessor : ICategoryProcessor {
        private readonly IUploadRequestRepository _repository;
        private readonly IFileStorage _blobStorage;
        private readonly IParsingFactory _factory;
        private readonly IBulkMerge<RawCategory> _bulk;

        public CategoryProcessor(IUploadRequestRepository repository, 
            IFileStorage blobStorage, 
            IParsingFactory factory, 
            IBulkMerge<RawCategory> bulk) {
            _repository = repository;
            _blobStorage = blobStorage;
            _factory = factory;
            _bulk = bulk;
        }

        public async Task ProcessAsync(UploadBaseMessage message) {
            var parser = await _factory.Get(message).ConfigureAwait(false);
            if (parser == null)
                return;
            var request = await _repository.GetAsync(message.UploadKey.ToString()).ConfigureAwait(false);
            var parents = new List<RawCategory>();
            var siblings = new List<RawCategory>();
            using (var stream = await _blobStorage.GetAsync(request.UploadRequestId.ToString()).ConfigureAwait(false)) {
                foreach (var entry in await parser.Parse<SourceCategory>(stream).ConfigureAwait(false)) {
                    if (string.IsNullOrEmpty(entry.ParentName)) {
                        parents.Add(Convert(entry));
                        continue;
                    }

                    siblings.Add(Convert(entry));
                }
            }

            await _bulk.MergeAsync(parents, siblings).ConfigureAwait(false);
        }

        private static RawCategory Convert(SourceCategory source) => new RawCategory {
            Name = source.Name,
            ParentName = source.ParentName
        };
    }
}