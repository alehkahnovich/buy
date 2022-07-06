using Buy.Infrastructure.Library.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Upload.Web
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.BootDependencies(typeof(Business.Bootstrap));
        }
    }
}