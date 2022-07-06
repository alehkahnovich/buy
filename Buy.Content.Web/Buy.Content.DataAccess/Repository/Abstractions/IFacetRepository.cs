using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Content.DataAccess.Domains.DTO;

namespace Buy.Content.DataAccess.Repository.Abstractions
{
    public interface IFacetRepository {
        Task<IEnumerable<Map>> GetMapAsync(int cat);
        Task<IEnumerable<Agg>> GetAttributesAsync(string prefix, int cat);
        Task<IEnumerable<Agg>> GetCategoriesAsync();
    }
}