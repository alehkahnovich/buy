using System.Threading.Tasks;
using Buy.Upload.Processor.DataAccess.Domain;

namespace Buy.Upload.Processor.DataAccess.Repositories.Abstractions
{
    public interface IUploadRequestRepository {
        Task<UploadRequest> GetAsync(string key);
    }
}