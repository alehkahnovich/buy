using Newtonsoft.Json;

namespace Buy.Content.Contract.Search.Facets
{
    public class FacetRangeNumber : Facet {
        [JsonProperty(RangeConstants.From, NullValueHandling = NullValueHandling.Ignore)]
        public double? FromValue { get; set; }
        [JsonProperty(RangeConstants.To, NullValueHandling = NullValueHandling.Ignore)]
        public double? ToValue { get; set; }
    }
}
