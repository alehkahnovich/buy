using System.Collections.Generic;

namespace Buy.UI.Web.Infrastructure.Settings
{
    internal sealed class OpenIdSettings {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public IEnumerable<string> Scopes { get; set; }
    }
}