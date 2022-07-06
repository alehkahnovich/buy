using System;
using System.Collections.Generic;
using Buy.Content.DataAccess.Domains.Abstractions;

namespace Buy.Content.DataAccess.Domains
{
    public sealed class Module : ITimeDomain {
        public int ModuleId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public IEnumerable<ModuleProperty> Properties { get; set; }
        public IEnumerable<int> Artifacts { get; set; }
    }
}