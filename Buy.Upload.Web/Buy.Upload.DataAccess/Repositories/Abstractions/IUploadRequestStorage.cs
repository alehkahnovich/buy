using System.Threading.Tasks;
using Buy.Upload.DataAccess.Domains;

namespace Buy.Upload.DataAccess.Repositories.Abstractions
{
    public interface IUploadRequestStorage {
        Task<Request> SaveAsync(Request request);
        Task<Request> GetAsync(int key);
        Task<bool> ExistsAsync(int key);
    }
}