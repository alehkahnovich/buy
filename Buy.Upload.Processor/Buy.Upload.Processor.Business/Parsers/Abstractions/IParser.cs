using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Buy.Upload.Processor.Business.Representation.SourceData.Abstractions;

namespace Buy.Upload.Processor.Business.Parsers.Abstractions
{
    public interface IParser {
        Task<IEnumerable<TRaw>> Parse<TRaw>(Stream stream) where TRaw : ISourceData;
    }
}