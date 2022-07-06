using System;
using System.Threading.Tasks;
using Buy.Idp.Business.Providers;
using Buy.Idp.Business.Validators.Errors;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Buy.Idp.Business.Validators
{
    public sealed class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator {
        private readonly IUserProvider _userProvider;
        public ResourceOwnerPasswordValidator(IUserProvider userProvider) {
            _userProvider = userProvider;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context) {
            var user = await _userProvider.GetByEmailAsync(context.UserName).ConfigureAwait(false);
            if (user == null || !string.Equals(user.Password, context.Password.Sha256(), StringComparison.Ordinal)) {
                context.Result = new GrantValidationResult {
                    IsError = true,
                    Error = $"{(int)ErrorCode.InvalidCredentials}"
                };
                return;
            }

            context.Result = new GrantValidationResult(user.UserId.ToString(), "Bearer");
        }
    }
}