using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Buy.Content.Business.Providers.Search.Population.Modules.Representation
{
    internal sealed class ModuleIndex {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("categories")]
        public List<int> Categories { get; set; }
        [JsonProperty("attributes")]
        public Dictionary<string, List<string>> Attributes { get; set; }
        [JsonProperty("artifacts")]
        public List<int> Artifacts { get; set; }
    }
}