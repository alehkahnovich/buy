using Buy.Idp.DataAccess.Connections;
using Buy.Idp.Infrastructure.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Idp.DataAccess
{
    public sealed class Bootstrap : IBootstrap {
        public void Register(IServiceCollection container) {
            container.AddSingleton<IConnectionFactory, ConnectionFactory>();
        }
    }
}