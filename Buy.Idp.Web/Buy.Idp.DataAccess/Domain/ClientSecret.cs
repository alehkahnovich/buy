using System;

namespace Buy.Idp.DataAccess.Domain
{
    public sealed class ClientSecret {
        public int Id { get; set; }
        public Guid ClientId { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
    }
}