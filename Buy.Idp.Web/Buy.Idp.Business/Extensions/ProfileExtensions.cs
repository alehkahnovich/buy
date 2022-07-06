using System;
using System.Collections.Generic;
using System.Linq;
using Buy.Idp.DataAccess.Domain;
using IdentityServer4.Models;
using SecurityClaim = System.Security.Claims.Claim;

namespace Buy.Idp.Business.Extensions
{
    public static class ProfileExtensions {
        private static readonly IDictionary<string, Func<User, string>> Claims = new Dictionary<string, Func<User, string>> {
            { "sub", user =>  user.UserId.ToString() },
            { "email", user =>  user.Email }
        };
        public static void AddRequestedClaims(this ProfileDataRequestContext context, User user) {
            context.AddRequestedClaims(context.RequestedClaimTypes.Select(claim => GetClaim(claim, user)).Where(claim => claim != null));
        }

        private static SecurityClaim GetClaim(string type, User user) {
            if (!Claims.ContainsKey(type)) return null;
            return new SecurityClaim(type, Claims[type](user));
        }
    }
}