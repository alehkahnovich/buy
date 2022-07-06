using System;

namespace Buy.Idp.DataAccess.Domain
{
    public sealed class UserGrant {
        public string GrantKey { get; set; }
        public string Type { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }
}