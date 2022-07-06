using Buy.Idp.Business.Providers;
using Buy.Idp.Infrastructure.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Data = Buy.Idp.DataAccess;

namespace Buy.Idp.Business
{
    public sealed class Bootstrap : IBootstrap {
        public void Register(IServiceCollection container) {
            new Data.Bootstrap().Register(container);
            container.AddScoped<IResourceProvider, ResourceProvider>();
            container.AddScoped<IUserProvider, UserProvider>();
        }
    }
}