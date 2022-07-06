using Serilog;

namespace Buy.Kafka.Infrastructure.Dependencies
{
    internal sealed class LoggerConfiguration {
        public static ILogger SetUp() => new Serilog.LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("processor-log-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
    }
}
