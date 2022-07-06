using System.Threading.Tasks;
using Buy.Idp.Business.Providers;
using Buy.Idp.Business.Extensions;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Buy.Idp.Business.Stores
{
    public sealed class ProfileService : IProfileService {
        private readonly IUserProvider _userProvider;

        public ProfileService(IUserProvider userProvider) {
            _userProvider = userProvider;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context) {
            var profile = await _userProvider.GetBySubjectAsync(context.Subject.GetSubjectId()).ConfigureAwait(false);
            context.AddRequestedClaims(profile);
        }

        public Task IsActiveAsync(IsActiveContext context) {
            //todo:
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}