using Buy.Infrastructure.Library.Dependencies;
using Buy.Upload.IO.Connections;
using Buy.Upload.IO.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Upload.IO
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.AddSingleton<IFileStorageConnectionFactory, FileStorageConnectionFactory>();
            container.AddSingleton<IAzureConnectionFactory, AzureConnectionFactory>();
            container.AddScoped<IFileStorage, AzureFileStorage>();
        }
    }
}