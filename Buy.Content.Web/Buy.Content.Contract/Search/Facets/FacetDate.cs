
using Newtonsoft.Json;
using System;

namespace Buy.Content.Contract.Search.Facets
{
    public class FacetDate : Facet {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime?[] Value { get; set; }
    }
}
