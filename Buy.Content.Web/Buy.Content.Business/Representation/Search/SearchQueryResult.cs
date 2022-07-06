using System.Collections.Generic;
using Buy.Content.Search.Core.Contracts.Abstractions;

namespace Buy.Content.Business.Representation.Search
{
    public sealed class SearchQueryResult<TEntry> where TEntry : ISearchUnit {
        public long Total { get; set; }
        public IEnumerable<TEntry> Results { get; set; }
        public IEnumerable<FacetContainer> Facets { get; set; }
    }
}