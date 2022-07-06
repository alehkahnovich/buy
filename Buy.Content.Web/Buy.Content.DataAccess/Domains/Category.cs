using System;
using Buy.Content.DataAccess.Domains.Abstractions;

namespace Buy.Content.DataAccess.Domains
{
    public class Category : ITimeDomain {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? WithSiblings { get; set; }
    }
}