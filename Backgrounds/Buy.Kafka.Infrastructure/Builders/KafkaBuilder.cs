using System.Reflection;
using Buy.Infrastructure.Library.Dependencies;
using Buy.Kafka.Infrastructure.Builders.Abstractions;
using Buy.Kafka.Infrastructure.Dependencies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Buy.Kafka.Infrastructure.Builders {
    public sealed class KafkaBuilder : IKafkaBuilder {
        private readonly IServiceCollection _container;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public KafkaBuilder() : this(new ServiceCollection(), 
            Dependencies.LoggerConfiguration.SetUp(), 
            SettingConfiguration.SetUp()) { }
        public KafkaBuilder(IServiceCollection conainer, ILogger logger, IConfiguration configuration) {
            _container = conainer;
            _logger = logger;
            _configuration = configuration;
        }

        public IKafkaHost Build() => new KafkaHost(_container, _configuration, _logger);

        public IKafkaBuilder ConfigureDependencies() {
            _container.BootDependencies(Assembly.GetCallingAssembly());
            return this;
        }

        public IKafkaBuilder ConfigureLogger() {
            _container.AddSingleton(_logger);
            return this;
        }

        public IKafkaBuilder ConfigureSettings() {
            _container.AddSingleton(_configuration);
            return this;
        }
    }
}
