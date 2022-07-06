using System;
using Buy.Content.DataAccess.Domains.Abstractions;

namespace Buy.Content.DataAccess.Domains.Extensions
{
    internal static class TimeDomainExtension {
        public static TDomain WithTimeStamp<TDomain>(this TDomain domain) where TDomain : ITimeDomain {
            var time = DateTime.UtcNow;
            domain.CreatedDate = time;
            domain.ModifiedDate = time;
            return domain;
        }
    }
}