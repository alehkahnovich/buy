using Nest;

namespace Buy.Content.Search.Core.Contracts.Facets
{
    public abstract class Property {
        [PropertyName("attr_id")]
        public string Id { get; set; }
        [PropertyName("type")]
        public string Type { get; set; }
    }
}