using Buy.Infrastructure.Library.Dependencies;
using Buy.Rasterization.IO.Connections;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Rasterization.IO
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.AddScoped<IStoreConnection, StoreConnection>();
        }
    }
}