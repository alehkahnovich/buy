using Newtonsoft.Json;
using System;

namespace Buy.Content.Contract.Search.Facets
{
    public class FacetRangeDate : Facet {
        [JsonProperty(RangeConstants.From, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? FromValue { get; set; }
        [JsonProperty(RangeConstants.To, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ToValue { get; set; }
    }
}
