using System;

namespace Buy.Idp.DataAccess.Domain.Views
{
    public sealed class ClientViewIdentifier {
        public Guid ClientId { get; }
        public string Name { get; }
        public bool ReuseRefresh { get; }
        public bool AllowOfflineAccess { get; }
        public ClientViewIdentifier(ClientView view) {
            ClientId = view.ClientId;
            Name = view.Name;
            ReuseRefresh = view.ReuseRefresh;
            AllowOfflineAccess = view.AllowOfflineAccess;
        }
    }
}