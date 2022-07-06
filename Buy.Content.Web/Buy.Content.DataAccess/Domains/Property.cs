using System;
using Buy.Content.DataAccess.Domains.Abstractions;

namespace Buy.Content.DataAccess.Domains
{
    public class Property : ITimeDomain {
        public int PropertyId { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Behavior { get; set; }
        public string Control { get; set; }
        public Property ParentProperty { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsFacet { get; set; }
        public int SortOrder { get; set; }
        public bool? WithSiblings { get; set; }
    }
}