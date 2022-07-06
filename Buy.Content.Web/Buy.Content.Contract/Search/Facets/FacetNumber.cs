using Newtonsoft.Json;

namespace Buy.Content.Contract.Search.Facets
{
    public class FacetNumber : Facet {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public double?[] Value { get; set; }
    }
}
