using System.Collections.Generic;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Module
{
    public sealed class ContentUnit {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("cat")]
        public int CategoryId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("artifacts")]
        public IEnumerable<int> Artifacts { get; set; }
        [JsonProperty("attributes")]
        public IEnumerable<ContentOption> Attributes { get; set; }
    }
}