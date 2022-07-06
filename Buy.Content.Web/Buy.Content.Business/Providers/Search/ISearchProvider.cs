using System.Threading.Tasks;
using Buy.Content.Business.Representation.Search;
using Buy.Content.Search.Core.Contracts;

namespace Buy.Content.Business.Providers.Search
{
    public interface ISearchProvider {
        Task<SearchQueryResult<Module>> SearchAsync(SearchQuery query);
    }
}