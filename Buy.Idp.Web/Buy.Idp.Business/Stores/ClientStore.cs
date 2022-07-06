using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Idp.DataAccess.Connections;
using Buy.Idp.DataAccess.Domain.Constants;
using Buy.Idp.DataAccess.Domain.Equality;
using Buy.Idp.DataAccess.Domain.Views;
using Buy.Idp.DataAccess.Representations.AccessType;
using Dapper;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Domain = Buy.Idp.DataAccess.Domain;
namespace Buy.Idp.Business.Stores
{
    public sealed class ClientStore : IClientStore {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IEqualityComparer<ClientViewIdentifier> _compare;
        public ClientStore(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
            _compare = new ClientViewIdentifierCompare();
        }

        public async Task<Client> FindClientByIdAsync(string clientId) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                var view = await connection.QueryMultipleAsync(
                        @"select * from IDP.vwClient where ClientId = @ClientId;
                          select * from IDP.ClientSecret where ClientId = @ClientId;
                          select * from IDP.ClientUri where ClientId = @ClientId",
                        new { ClientId = clientId })
                .ConfigureAwait(false);

                var client = await view.ReadAsync<ClientView>().ConfigureAwait(false);
                var secrets = await view.ReadAsync<Domain.ClientSecret>().ConfigureAwait(false);
                var uris = await view.ReadAsync<Domain.ClientRedirectUri>().ConfigureAwait(false);

                var result = client
                    .GroupBy(entry => new ClientViewIdentifier(entry), _compare)
                    .Select(entry => new Client {
                        ClientId = entry.Key.ClientId.ToString(),
                        ClientName = entry.Key.Name,
                        AllowedGrantTypes = GrantTypes.Hybrid,
                        AllowOfflineAccess = entry.Key.AllowOfflineAccess,
                        RefreshTokenUsage = entry.Key.ReuseRefresh ? TokenUsage.ReUse : TokenUsage.OneTimeOnly,
                        AllowedScopes = entry.Select(inner => inner.ResourceName).ToList(),
                        RedirectUris = new List<string>(),
                        RequireConsent = false,
                        PostLogoutRedirectUris = new List<string>() 
                    }).SingleOrDefault();

                if (result == null)
                    return null;

                result.ClientSecrets = secrets.Select(entry => new Secret(entry.Value, entry.Expiration)).ToList();

                foreach (var uri in uris) {
                    if (string.Equals(uri.Type, DomainConstants.ClientUri.Redirect, System.StringComparison.Ordinal))
                        result.RedirectUris.Add(uri.Uri);
                    if (string.Equals(uri.Type, DomainConstants.ClientUri.PostLogout, System.StringComparison.Ordinal))
                        result.PostLogoutRedirectUris.Add(uri.Uri);
                }

                return result;
            }
        }
    }
}