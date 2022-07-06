using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Idp.Business.Providers;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Buy.Idp.Business.Stores
{
    public sealed class ResourceStore : IResourceStore {
        private readonly IResourceProvider _resourceProvider;

        public ResourceStore(IResourceProvider resourceProvider) {
            _resourceProvider = resourceProvider;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames) {
            var resources = await _resourceProvider.GetIdentityResourcesAsync().ConfigureAwait(false);
            return resources.Where(entry => scopeNames.Contains(entry.Name, StringComparer.Ordinal)).Select(Convert);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames) {
            var resources = await _resourceProvider.GetResourcesAsync().ConfigureAwait(false);
            return resources.Where(entry => scopeNames.Contains(entry.Name, StringComparer.Ordinal)).Select(Convert);
        }

        public async Task<ApiResource> FindApiResourceAsync(string name) {
            var resources = await _resourceProvider.GetResourcesAsync().ConfigureAwait(false);
            var resource = resources.SingleOrDefault(entry => string.Equals(entry.Name, name, StringComparison.Ordinal));
            return resource == null ? null : Convert(resource);
        }

        public async Task<Resources> GetAllResourcesAsync() {
            var resources = await _resourceProvider.GetResourcesAsync().ConfigureAwait(false);
            var identity = await _resourceProvider.GetIdentityResourcesAsync().ConfigureAwait(false);
            return new Resources {
                ApiResources = resources.Select(Convert).ToList(),
                IdentityResources = identity.Select(Convert).ToList()
            };
        }

        private static ApiResource Convert(DataAccess.Domain.Resource resource) => 
            new ApiResource(resource.Name, resource.Description);

        private static IdentityResource Convert(DataAccess.Domain.IdentityResource resource) =>
            new IdentityResource(resource.Name, resource.Description, resource.Claims.Select(entry => entry.Name));
    }
}