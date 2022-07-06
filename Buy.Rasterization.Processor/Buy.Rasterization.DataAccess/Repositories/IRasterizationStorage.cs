using System.Threading.Tasks;
using Buy.Rasterization.DataAccess.Domain;

namespace Buy.Rasterization.DataAccess.Repositories
{
    public interface IRasterizationStorage {
        Task<RasterizationRequest> GetAsync(int id);
    }
}