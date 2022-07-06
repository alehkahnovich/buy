using Buy.Infrastructure.Library.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Infrastructure.Library.Tests
{
    public class Bootstrap : IBootstrap {
        private sealed class Dummy { }
        public void Boot(IServiceCollection container) {
            container.Add(ServiceDescriptor.Singleton<Dummy, Dummy>());
        }
    }
}