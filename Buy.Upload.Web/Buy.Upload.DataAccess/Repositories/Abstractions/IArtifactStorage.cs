using System.Threading.Tasks;
using Buy.Upload.DataAccess.Domains;

namespace Buy.Upload.DataAccess.Repositories.Abstractions
{
    public interface IArtifactStorage {
        Task<Artifact> GetAsync(string type, int request);
        Task<Artifact> GetAsync(int id);
    }
}