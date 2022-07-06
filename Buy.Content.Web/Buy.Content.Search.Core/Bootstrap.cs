using Buy.Content.Search.Core.Connections;
using Buy.Infrastructure.Library.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Content.Search.Core
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.AddSingleton<IEngineConnection, EngineConnection>();
        }
    }
}