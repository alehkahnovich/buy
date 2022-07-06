using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Idp.DataAccess.Connections;
using Buy.Idp.DataAccess.Domain;
using Buy.Idp.DataAccess.Domain.Constants;
using Buy.Idp.DataAccess.Representations.AccessType;
using Dapper;

namespace Buy.Idp.Business.Providers
{
    public sealed class ResourceProvider : IResourceProvider {
        private readonly IConnectionFactory _connectionFactory;

        public ResourceProvider(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Resource>> GetResourcesAsync() => await GetResource(DomainConstants.ResourceType.Api).ConfigureAwait(false);

        public async Task<IEnumerable<IdentityResource>> GetIdentityResourcesAsync() => await GetIdentityResource(DomainConstants.ResourceType.Identity).ConfigureAwait(false);

        private async Task<IEnumerable<Resource>> GetResource(string type) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.QueryAsync<Resource>("select * from IDP.Resource where Type = @Type", new { Type = type }).ConfigureAwait(false);
            }
        }

        private async Task<IEnumerable<IdentityResource>> GetIdentityResource(string type) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                var dictionary = new Dictionary<int, IdentityResource>();
                await connection.QueryAsync<Resource, Claim, IdentityResource>(
                    @"select r.ResourceId, r.Name, r.Description, c.ClaimId, c.Name
                    from IDP.Resource as r
                    inner join IDP.ResourceClaim as cr on r.ResourceId = cr.ResourceId
                    inner join IDP.Claim as c on c.ClaimId = cr.ClaimId
                    where r.Type = @Type", 
                    (resource, claim) => {
                        if (!dictionary.ContainsKey(resource.ResourceId)) {
                            var identity = new IdentityResource {
                                Name = resource.Name,
                                Description = resource.Description,
                                ResourceId = resource.ResourceId,
                                Type = resource.Type,
                                Claims = new List<Claim> {
                                    new Claim { ClaimId = claim.ClaimId, Name = claim.Name }
                                }
                            };
                            dictionary.Add(resource.ResourceId, identity);
                            return identity;
                        }

                        var current = dictionary[resource.ResourceId];

                        current.Claims.Add(new Claim { ClaimId = claim.ClaimId, Name = claim.Name });

                        return current;
                    }, 
                    new { Type = type }, splitOn:nameof(Claim.ClaimId))
                .ConfigureAwait(false);

                return dictionary.Values;
            }
        }
    }
}