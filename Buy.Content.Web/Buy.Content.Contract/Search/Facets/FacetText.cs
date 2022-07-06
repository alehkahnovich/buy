using Newtonsoft.Json;

namespace Buy.Content.Contract.Search.Facets
{
    public class FacetText : Facet {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Value { get; set; }
    }
}
