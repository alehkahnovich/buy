using Buy.Infrastructure.Library.Dependencies;
using Buy.Upload.DataAccess.Connections;
using Buy.Upload.DataAccess.Repositories;
using Buy.Upload.DataAccess.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Upload.DataAccess
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.AddSingleton<IConnectionFactory, ConnectionFactory>();
            container.AddScoped<IUploadRequestStorage, UploadRequestStorage>();
            container.AddScoped<IArtifactStorage, ArtifactStorage>();
        }
    }
}