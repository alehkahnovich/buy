using System;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Content
{
    public sealed class Property {
        [JsonProperty("key")]
        public int? PropertyId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("parent_key")]
        public int? ParentId { get; set; }
        [JsonProperty("parent_name")]
        public string ParentName { get; set; }
        [JsonProperty("modified_date")]
        public DateTime ModifiedDate { get; set; }
        [JsonProperty("with_siblings")]
        public bool? WithSiblings { get; set; }
        [JsonProperty("is_facet")]
        public bool IsFacet { get; set; }
    }
}