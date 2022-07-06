using System.Collections.Generic;

namespace Buy.Content.Business.Representation.Module
{
    public sealed class ContentUnit {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ContentUnitOption> Attributes { get; set; }
        public IEnumerable<int> Artifacts { get; set; }
    }
}