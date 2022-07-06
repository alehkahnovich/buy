using Microsoft.Extensions.DependencyInjection;

namespace Buy.Idp.Infrastructure.Dependencies
{
    public static class BootstrapExtensions {
        public static IServiceCollection BootDependencies(this IServiceCollection collection, IBootstrap bootstrap) {
            bootstrap.Register(collection);
            return collection;
        }
    }
}