using System.Collections.Generic;
using Buy.Content.Contract.Search.Facets;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Search
{
    public class SearchQueryResult<TEntry> {
        [JsonProperty("total")]
        public long Total { get; set; }
        [JsonProperty("results")]
        public IEnumerable<TEntry> Results { get; set; }
        [JsonProperty("facets")]
        public IEnumerable<FacetContainer> Facets { get; set; }
    }
}