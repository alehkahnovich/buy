using Buy.Infrastructure.Web.Handlers;
using Buy.Infrastructure.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Infrastructure.Web.Extensions
{
    public static class InfrastructureExtensions {
        public static IServiceCollection AddExceptionHandling(this IServiceCollection container) {
            container.AddTransient<IExceptionHandler, ExceptionHandler>();
            return container;
        }

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder) {
            builder.UseMiddleware<ExceptionMiddleware>();
            return builder;
        }
    }
}