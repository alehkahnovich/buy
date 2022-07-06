using System.Linq;
using Buy.Content.Contract.Content;
using Buy.Content.Contract.Search.Facets;

namespace Buy.Content.Business.Converters
{
    public static class ModuleConverter {
        public static Module Convert(this Search.Core.Contracts.Module module) {
            return new Module {
                Id = module.Id,
                Name = module.Name,
                Description = module.Description,
                CreatedDate = module.CreatedDate,
                Artifacts = module.Artifacts,
                Facets = module.Properties?.Properties.Select(property => (Facet)FacetConverter.Convert((dynamic)property)).ToList()
            };
        }
    }
}