using Buy.Infrastructure.Library.Dependencies;
using Buy.Rasterization.Business.Converters;
using Buy.Rasterization.Business.Converters.Abstractions;
using Buy.Rasterization.Business.Resizers;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Rasterization.Business
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.BootDependencies(typeof(DataAccess.Bootstrap));
            container.BootDependencies(typeof(IO.Bootstrap));
            container.AddScoped<IThumbnail, Thumbnail>();
            container.AddScoped<IResizer, Resizer>();
        }
    }
}