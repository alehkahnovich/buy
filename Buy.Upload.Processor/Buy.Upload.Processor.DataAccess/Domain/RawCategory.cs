using System;

namespace Buy.Upload.Processor.DataAccess.Domain
{
    public class RawCategory {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}