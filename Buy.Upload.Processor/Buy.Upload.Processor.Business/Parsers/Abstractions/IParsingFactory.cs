using System.Threading.Tasks;
using Buy.Upload.Processor.Messages.Abstractions;

namespace Buy.Upload.Processor.Business.Parsers.Abstractions
{
    public interface IParsingFactory {
        Task<IParser> Get(UploadBaseMessage message);
    }
}