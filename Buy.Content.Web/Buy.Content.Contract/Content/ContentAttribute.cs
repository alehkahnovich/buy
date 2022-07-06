using System.Collections.Generic;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Content
{
    public class ContentAttribute {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("behavior", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Behavior { get; set; }
        [JsonProperty("control", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Control { get; set; }
        [JsonProperty("options")]
        public IEnumerable<ContentAttribute> Siblings { get; set; }
    }
}