using System.Collections.Generic;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Search.Facets
{
    public sealed class FacetContainer {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("facets", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<Facet> Facets { get; set; }
    }
}
