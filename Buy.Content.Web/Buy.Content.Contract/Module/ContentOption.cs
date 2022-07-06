using System.Collections.Generic;
using Newtonsoft.Json;

namespace Buy.Content.Contract.Module
{
    public class ContentOption {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("values")]
        public IEnumerable<ContentOptionValue> Values { get; set; }
    }
}