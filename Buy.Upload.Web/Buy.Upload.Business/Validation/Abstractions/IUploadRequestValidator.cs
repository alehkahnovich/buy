using System.Threading.Tasks;
using Buy.Upload.Business.Contracts;

namespace Buy.Upload.Business.Validation.Abstractions
{
    public interface IUploadRequestValidator {
        Task<bool> ValidateAsync(UploadRequest request);
    }
}