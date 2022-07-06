using Buy.Infrastructure.Library.Dependencies;
using Buy.Upload.Processor.DataAccess.Connections;
using Buy.Upload.Processor.DataAccess.Repositories;
using Buy.Upload.Processor.DataAccess.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Upload.Processor.DataAccess
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.AddSingleton<IConnectionFactory, ConnectionFactory>();
            container.AddScoped<IUploadRequestRepository, UploadRequestRepository>();
        }
    }
}