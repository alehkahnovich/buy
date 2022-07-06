using Buy.Content.Business.Representation.Search;
using System.Linq;

namespace Buy.Content.Business.Converters
{
    public static class FacetContainerConvert {
        public static Contract.Search.Facets.FacetContainer Convert(FacetContainer container) {
            return new Contract.Search.Facets.FacetContainer {
                Key = container.Key,
                Name = container.Name,
                Facets = container.Facets?.Select(facet => (Contract.Search.Facets.Facet)FacetConverter.Convert((dynamic)facet))
            };
        }
    }
}
