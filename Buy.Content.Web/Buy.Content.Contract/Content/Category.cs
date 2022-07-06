using System;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Content
{
    public sealed class Category {
        [JsonProperty("key")]
        public int? CategoryId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("parent_key")]
        public int? ParentId { get; set; }
        [JsonProperty("parent_name")]
        public string ParentName { get; set; }
        [JsonProperty("modified_date")]
        public DateTime ModifiedDate { get; set; }
        [JsonProperty("with_siblings")]
        public bool? WithSiblings { get; set; }
    }
}