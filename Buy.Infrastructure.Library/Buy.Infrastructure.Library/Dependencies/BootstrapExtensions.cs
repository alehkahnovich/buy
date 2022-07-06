using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Infrastructure.Library.Dependencies
{
    public static class BootstrapExtensions {
        public static IServiceCollection BootDependencies(this IServiceCollection container, Type type) => Boot(container, type.Assembly);

        public static IServiceCollection BootDependencies(this IServiceCollection container) => Boot(container, Assembly.GetCallingAssembly());

        public static IServiceCollection BootDependencies(this IServiceCollection container, Assembly assembly) => Boot(container, assembly);

        private static IServiceCollection Boot(IServiceCollection container, Assembly assembly) {
            var bootstraps = assembly.GetTypes().Where(src => typeof(IBootstrap).IsAssignableFrom(src));
            foreach (var bootstrap in bootstraps) {
                if (!(Activator.CreateInstance(bootstrap) is IBootstrap instance)) continue;
                instance.Boot(container);
            }
            return container;
        }
    }
}