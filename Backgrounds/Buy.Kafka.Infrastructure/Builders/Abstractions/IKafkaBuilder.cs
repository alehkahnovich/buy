namespace Buy.Kafka.Infrastructure.Builders.Abstractions
{
    public interface IKafkaBuilder {
        IKafkaBuilder ConfigureLogger();
        IKafkaBuilder ConfigureSettings();
        IKafkaBuilder ConfigureDependencies();
        IKafkaHost Build();
    }
}
