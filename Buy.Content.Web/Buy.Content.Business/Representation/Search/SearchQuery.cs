using System.Collections.Generic;

namespace Buy.Content.Business.Representation.Search
{
    public sealed class SearchQuery {
        public string Term { get; set; }
        public int From { get; set; }
        public int Size { get; set; }
        public int? Category { get; set; }
        public IEnumerable<Facet> Facets { get; set; } = new List<Facet>();
    }
}