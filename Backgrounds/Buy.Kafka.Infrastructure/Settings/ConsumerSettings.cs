namespace Buy.Kafka.Infrastructure.Settings
{
    internal sealed class ConsumerSettings {
        public string GroupId { get; set; }
        public string BootstrapServers { get; set; }
        public bool? EnableAutoCommit { get; set; }
        public int? SessionTimeoutMs { get; set; }
    }
}
