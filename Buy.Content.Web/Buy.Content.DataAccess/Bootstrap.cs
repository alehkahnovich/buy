using Buy.Content.DataAccess.Connections;
using Buy.Content.DataAccess.Repository;
using Buy.Content.DataAccess.Repository.Abstractions;
using Buy.Infrastructure.Library.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Content.DataAccess
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.AddSingleton<IConnectionFactory, ConnectionFactory>();
            container.AddScoped<ICategoryRepository, CategoryRepository>();
            container.AddScoped<IPropertyRepository, PropertyRepository>();
            container.AddScoped<IFacetRepository, FacetRepository>();
            container.AddScoped<IModuleRepository, ModuleRepository>();
        }
    }
}