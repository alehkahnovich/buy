using System.Collections.Generic;
using Buy.Idp.DataAccess.Domain.Abstractions;

namespace Buy.Idp.DataAccess.Domain
{
    public class IdentityResource : IResource {
        public int ResourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}