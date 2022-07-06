using System.Collections.Generic;

namespace Buy.Content.Business.Representation.Search
{
    public sealed class Category {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Count { get; set; }
        public IEnumerable<Category> Siblings { get; set; }
    }
}