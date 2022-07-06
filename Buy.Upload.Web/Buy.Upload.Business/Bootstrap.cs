using Buy.Infrastructure.Library.Dependencies;
using Buy.Kafka.Consumers;
using Buy.Upload.Business.Services;
using Buy.Upload.Business.Services.Abstractions;
using Buy.Upload.Business.Validation;
using Buy.Upload.Business.Validation.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Upload.Business
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.BootDependencies(typeof(IO.Bootstrap));
            container.BootDependencies(typeof(DataAccess.Bootstrap));
            container.AddScoped<IUploadService, UploadService>();
            container.AddScoped<IUploadRequestValidator, UploadRequestValidator>();
            container.AddScoped<IArtifactService, ArtifactService>();
            container.BootDependencies(typeof(Kafka.Consumers.Bootstrap));
            container.AddScoped<IProducer, Producer>();
        }
    }
}