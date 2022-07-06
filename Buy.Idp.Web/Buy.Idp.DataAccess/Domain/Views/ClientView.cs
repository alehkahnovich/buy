using System;

namespace Buy.Idp.DataAccess.Domain.Views
{
    public sealed class ClientView {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public bool ReuseRefresh { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public string ResourceName { get; set; }
        public string ResourceDescription { get; set; }
    }
}