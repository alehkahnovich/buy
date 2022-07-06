using System.Collections.Generic;

namespace Buy.Content.Search.Core.Contracts.Facets
{
    public sealed class Facet {
        public IEnumerable<Property> Properties { get; set; }
    }
}
