using System.Threading.Tasks;
using Buy.Rasterization.DataAccess.Domain;

namespace Buy.Rasterization.DataAccess.Repositories
{
    public interface IArtifactStorage {
        Task<Artifact> SaveAsync(Artifact artifact);
    }
}