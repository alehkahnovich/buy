using System.Collections.Generic;

namespace Buy.Content.Business.Representation.Search
{
    public abstract class Facet {
        public string Id { get; set; }
        public long? Count { get; set; }
        public abstract string Type { get; }
        public IEnumerable<Facet> Facets { get; set; }
    }
}