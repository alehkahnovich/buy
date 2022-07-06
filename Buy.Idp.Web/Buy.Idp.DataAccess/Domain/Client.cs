using System;

namespace Buy.Idp.DataAccess.Domain
{
    public sealed class Client {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public bool ReuseRefresh { get; set; }
        public bool AllowOfflineAccess { get; set; }
    }
}