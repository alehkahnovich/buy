using System;

namespace Buy.Idp.Web.Infrastructure
{
    public sealed class AccountSettings {
        private const int Hour = 1;
        public static DateTimeOffset GetExpiresUtc() => DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(Hour));
    }
}