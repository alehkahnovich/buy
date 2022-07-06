using Buy.Content.Contract.Search;
using System.Linq;
using Buy.Content.Contract.Content;
using BL = Buy.Content.Business.Representation;

namespace Buy.Content.Business.Converters
{
    public static class QueryConverter {
        public static BL.Search.SearchQuery Convert(this SearchQuery query) {
            return new BL.Search.SearchQuery {
                Term = query.Term,
                From = query.From,
                Size = query.Size,
                Category = query.Category,
                Facets = query.Facets?.Select(facet => (BL.Search.Facet)FacetConverter.Convert((dynamic)facet)).ToList()
            };
        }

        public static SearchQueryResult<Module> Convert(this BL.Search.SearchQueryResult<Search.Core.Contracts.Module> query) {
            return new SearchQueryResult<Module> {
                Total = query.Total,
                Results = query.Results?.Select(ModuleConverter.Convert),
                Facets = query.Facets?.Select(FacetContainerConvert.Convert)
            };
        }
    }
}