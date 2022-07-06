namespace Buy.Idp.DataAccess.Domain.Constants
{
    public sealed class DomainConstants {
        public sealed class ClientUri {
            public const string Redirect = nameof(Redirect);
            public const string PostLogout = nameof(PostLogout);
        }

        public sealed class ResourceType {
            public const string Api = nameof(Api);
            public const string Identity = nameof(Identity);
        }
    }
}
