using Buy.Idp.DataAccess.Domain.Abstractions;

namespace Buy.Idp.DataAccess.Domain
{
    public sealed class Resource : IResource {
        public int ResourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}