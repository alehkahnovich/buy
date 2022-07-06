using Buy.Infrastructure.Library.Dependencies;
using Buy.Rasterization.DataAccess.Connections;
using Buy.Rasterization.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Rasterization.DataAccess
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.AddSingleton<IConnectionFactory, ConnectionFactory>();
            container.AddScoped<IRasterizationStorage, RasterizationStorage>();
            container.AddScoped<IArtifactStorage, ArtifactStorage>();
        }
    }
}