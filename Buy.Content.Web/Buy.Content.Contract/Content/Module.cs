using System;
using System.Collections.Generic;
using Buy.Content.Contract.Search.Facets;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Content
{
    public sealed class Module {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("attributes")]
        public IEnumerable<Facet> Facets { get; set; }
        [JsonProperty("artifacts")]
        public IEnumerable<int> Artifacts { get; set; }
    }
}