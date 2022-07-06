using Buy.Content.Business.Providers;
using Buy.Content.Business.Providers.Abstractions;
using Buy.Content.Business.Providers.Search;
using Buy.Content.Business.Providers.Search.Builders;
using Buy.Content.Business.Providers.Search.Population.Modules;
using Buy.Infrastructure.Library.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Content.Business
{
    public class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.BootDependencies(typeof(DataAccess.Bootstrap));
            container.BootDependencies(typeof(Search.Core.Bootstrap));
            container.AddScoped<ICategoryProvider, CategoryProvider>();
            container.AddScoped<IPropertyProvider, PropertyProvider>();
            container.AddScoped<IContentUnitProvider, ContentUnitProvider>();

            RegisterSearch(container);
        }

        private static void RegisterSearch(IServiceCollection container) {
            container.AddScoped<ISearchProvider, SearchProvider>();
            container.AddScoped<ISearchQueryBuilder, SearchQueryBuilder>();
            container.AddScoped<IAggregationBuilder, AggregationBuilder>();
            container.AddScoped<IBucketProvider, BucketProvider>();

            container.AddScoped<IModuleIndexProvider, ModuleIndexProvider>();
        }
    }
}