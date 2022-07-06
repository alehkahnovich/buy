using System;

namespace Buy.Upload.Processor.DataAccess.Domain
{
    public sealed class Category {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}