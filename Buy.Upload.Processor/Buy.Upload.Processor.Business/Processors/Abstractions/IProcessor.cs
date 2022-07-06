using System.Threading.Tasks;
using Buy.Upload.Processor.Messages.Abstractions;

namespace Buy.Upload.Processor.Business.Processors.Abstractions
{
    public interface IProcessor {
        Task ProcessAsync(UploadBaseMessage message);
    }
}