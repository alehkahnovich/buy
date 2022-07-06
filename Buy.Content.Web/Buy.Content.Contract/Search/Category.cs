using System.Collections.Generic;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Search
{
    public sealed class Category {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("count")]
        public long Count { get; set; }
        [JsonProperty("siblings")]
        public IEnumerable<Category> Categories { get; set; }
    }
}