using Microsoft.Extensions.DependencyInjection;

namespace Buy.Idp.Infrastructure.Dependencies
{
    public interface IBootstrap {
        void Register(IServiceCollection container);
    }
}