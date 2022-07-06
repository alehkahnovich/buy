using System.Threading.Tasks;
using Buy.Idp.Web.Infrastructure;
using Buy.Idp.Web.Representation;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Buy.Idp.Web.Controllers
{
    [AllowAnonymous]
    public sealed class AccountController : Controller {
        private readonly IResourceOwnerPasswordValidator _resourceOwnerPasswordValidator;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IEventService _eventService;

        public AccountController(IResourceOwnerPasswordValidator resourceOwnerPasswordValidator, 
            IIdentityServerInteractionService interactionService, 
            IEventService eventService) {
            _resourceOwnerPasswordValidator = resourceOwnerPasswordValidator;
            _interactionService = interactionService;
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl) {
            var context = await _interactionService.GetAuthorizationContextAsync(returnUrl);
            
            return View(new Login {
                ReturnUrl = returnUrl,
                Client = context?.ClientId
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model) {
            var context = await _interactionService.GetAuthorizationContextAsync(model.ReturnUrl);
            if (!ModelState.IsValid)
                return await Login(model.ReturnUrl);

            var pwdContext = new ResourceOwnerPasswordValidationContext {
                Password = model.Password,
                UserName = model.Username
            };

            await _resourceOwnerPasswordValidator.ValidateAsync(pwdContext);

            if (pwdContext.Result.IsError) {
                await _eventService.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context?.ClientId));
                ModelState.AddModelError(string.Empty, "invalid credentials");
                return await Login(model.ReturnUrl);
            }

            await _eventService.RaiseAsync(new UserLoginSuccessEvent(pwdContext.UserName, pwdContext.Result.Subject.GetSubjectId(), pwdContext.UserName, clientId: context?.ClientId));

            var props = new AuthenticationProperties {
                ExpiresUtc = AccountSettings.GetExpiresUtc(),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(pwdContext.Result.Subject.GetSubjectId(), pwdContext.UserName, props);

            if (string.IsNullOrEmpty(model.ReturnUrl))
                return Redirect("~/");

            if (Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);
            
            return await Login(model.ReturnUrl);
        }
    }
}