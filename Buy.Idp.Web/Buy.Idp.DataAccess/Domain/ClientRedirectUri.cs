using System;

namespace Buy.Idp.DataAccess.Domain
{
    public sealed class ClientRedirectUri {
        public int Id { get; set; }
        public Guid ClientId { get; set; }
        public string Uri { get; set; }
        public string Type { get; set; }
    }
}