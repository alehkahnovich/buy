using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Content.DataAccess.Domains;

namespace Buy.Content.DataAccess.Repository.Abstractions
{
    public interface ICategoryRepository : IRepositoryUnit<int, Category> {
        Task<IEnumerable<Category>> GetLightweightAsync();
    }
}