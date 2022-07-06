using Buy.Content.Contract.Search.Facets.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Buy.Content.Contract.Search.Facets
{
    [JsonConverter(typeof(FacetConverter))]
    public abstract class Facet {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("facets", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Facet> Facets { get; set; }
    }
}
