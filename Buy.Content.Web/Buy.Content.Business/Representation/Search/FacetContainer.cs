using System.Collections.Generic;

namespace Buy.Content.Business.Representation.Search
{
    public sealed class FacetContainer {
        public string Key { get; set; }
        public string Name { get; set; }
        public IEnumerable<Facet> Facets { get; set; }
    }
}
