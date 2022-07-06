using Newtonsoft.Json;

namespace Buy.Content.Contract.Module
{
    public class ContentOptionValue {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}