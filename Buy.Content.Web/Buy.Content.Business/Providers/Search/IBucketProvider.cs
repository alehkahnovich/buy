using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Content.Business.Representation.Search;

namespace Buy.Content.Business.Providers.Search
{
    public interface IBucketProvider {
        Task<IEnumerable<Category>> GetAsync();
    }
}