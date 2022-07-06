using System.Collections.Generic;

namespace Buy.Content.Business.Representation.Content
{
    public class ContentAttribute {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Behavior { get; set; }
        public string Control { get; set; }
        public string Type { get; set; }
        public IEnumerable<ContentAttribute> Siblings { get; set; }
    }
}