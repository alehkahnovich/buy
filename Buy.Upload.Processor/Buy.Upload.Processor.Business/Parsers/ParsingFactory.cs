using System;
using System.IO;
using System.Threading.Tasks;
using Buy.Upload.Processor.Business.Extensions;
using Buy.Upload.Processor.Business.Parsers.Abstractions;
using Buy.Upload.Processor.Business.Parsers.Types;
using Buy.Upload.Processor.DataAccess.Repositories.Abstractions;
using Buy.Upload.Processor.Messages.Abstractions;
using Serilog;

namespace Buy.Upload.Processor.Business.Parsers
{
    public sealed class ParsingFactory : IParsingFactory {
        private readonly IUploadRequestRepository _storage;
        private readonly ILogger _logger;
        private readonly Func<ParserType, IParser> _factory;
        public ParsingFactory(Func<ParserType, IParser> factory, IUploadRequestRepository storage, ILogger logger) {
            _factory = factory;
            _storage = storage;
            _logger = logger;
        }

        public async Task<IParser> Get(UploadBaseMessage message) {
            var request = await _storage.GetAsync(message.UploadKey.ToString()).ConfigureAwait(false);
            if (string.IsNullOrEmpty(request?.FileName)) {
                _logger.Warning($"UploadRequest {message.UploadKey} is empty or FileName is not specified");
                return null;
            }

            var extension = Path.GetExtension(request.FileName);

            foreach (ParserType type in Enum.GetValues(typeof(ParserType))) {
                if (!string.Equals(type.GetEnumDescription(), extension, StringComparison.OrdinalIgnoreCase)) continue;
                return _factory(type);
            }

            _logger.Warning($"No parser found for extension {extension}");
            return null;
        }
    }
}