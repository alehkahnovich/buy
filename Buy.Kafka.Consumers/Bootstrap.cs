using Buy.Infrastructure.Library.Dependencies;
using Buy.Kafka.Consumers.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Kafka.Consumers {
    public sealed class Bootstrap : IBootstrap {
        public void Boot(IServiceCollection container) {
            container.AddSingleton<IProducerSettings, ProducerSettingsProvider>();
        }
    }
}
