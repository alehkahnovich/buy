using Microsoft.Extensions.Configuration;
using System;

namespace Buy.Kafka.Consumers.Settings
{
    public class ProducerSettingsProvider : IProducerSettings {
        private readonly ProducerSettings _settings;
        public ProducerSettingsProvider(IConfiguration configuration) => _settings = configuration.GetSection("Producer").Get<ProducerSettings>();
        public ProducerSettings Get() => _settings;
    }
}
