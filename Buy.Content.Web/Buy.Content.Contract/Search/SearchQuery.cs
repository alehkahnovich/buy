using Buy.Content.Contract.Search.Facets;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Buy.Content.Contract.Search
{
    public class SearchQuery {
        private const int Default = 30;
        private const int Max = 100;
        [JsonProperty("term")]
        public string Term { get; set; }
        [JsonProperty("from")]
        public int From { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; } = Default;
        [JsonProperty("cat")]
        public int? Category { get; set; }
        [JsonProperty("facets")]
        public IEnumerable<Facet> Facets { get; set; }
        public bool IsValid() => Size <= Max;
    }
}
