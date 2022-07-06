using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Idp.DataAccess.Domain;

namespace Buy.Idp.Business.Providers {
    public interface IResourceProvider {
        Task<IEnumerable<Resource>> GetResourcesAsync();
        Task<IEnumerable<IdentityResource>> GetIdentityResourcesAsync();
    }
}