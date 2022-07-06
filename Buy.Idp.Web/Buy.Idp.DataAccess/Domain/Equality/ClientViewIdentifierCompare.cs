using System.Collections.Generic;
using Buy.Idp.DataAccess.Domain.Views;

namespace Buy.Idp.DataAccess.Domain.Equality
{
    public sealed class ClientViewIdentifierCompare : IEqualityComparer<ClientViewIdentifier> {
        public bool Equals(ClientViewIdentifier left, ClientViewIdentifier right) => left?.ClientId == right?.ClientId;
        public int GetHashCode(ClientViewIdentifier view) => view.ClientId.GetHashCode();
    }
}