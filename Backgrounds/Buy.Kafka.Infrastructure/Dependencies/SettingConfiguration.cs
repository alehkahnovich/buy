using Microsoft.Extensions.Configuration;
namespace Buy.Kafka.Infrastructure.Dependencies {
    internal sealed class SettingConfiguration {
        public static IConfiguration SetUp() {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
#if DEBUG
                .AddJsonFile("appsettings.Development.json", true, true)
#endif
                .Build();
        }
    }
}
