using System;

namespace Buy.Idp.DataAccess.Domain
{
    public sealed class ClientScope {
        public Guid ClientId { get; set; }
        public int ResourceId { get; set; }
    }
}