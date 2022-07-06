using System.Threading.Tasks;
using Buy.Upload.Business.Contracts;
using Buy.Upload.Contracts.Uploads;

namespace Buy.Upload.Business.Services.Abstractions
{
    public interface IUploadService {
        Task<UploadedRequest> UploadAsync(UploadRequest request);
        Task<UploadRequest> GetAsync(int key);
    }
}