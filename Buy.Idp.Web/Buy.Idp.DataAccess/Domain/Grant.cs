using System;

namespace Buy.Idp.DataAccess.Domain
{
    public sealed class Grant {
        public string Key { get; set; }
        public string Type { get; set; }
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }
}