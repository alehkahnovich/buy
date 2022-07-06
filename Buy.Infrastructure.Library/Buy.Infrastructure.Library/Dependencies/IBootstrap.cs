using Microsoft.Extensions.DependencyInjection;

namespace Buy.Infrastructure.Library.Dependencies
{
    public interface IBootstrap {
        void Boot(IServiceCollection container);
    }
}