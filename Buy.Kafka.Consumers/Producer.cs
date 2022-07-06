using Buy.Kafka.Consumers.Serialization;
using Buy.Kafka.Consumers.Settings;
using Confluent.Kafka;
using System.Threading.Tasks;

namespace Buy.Kafka.Consumers
{
    public sealed class Producer : IProducer {
        private readonly ProducerSettings _settings;
        public Producer(IProducerSettings settings) {
            _settings = settings.Get();
        }
        public async Task ProduceAsync<TKey, TValue>(string topic, TKey key, TValue message) where TValue : class {
            var configuration = new ProducerConfig { BootstrapServers = _settings.BootstrapServers };
            var builder = new ProducerBuilder<TKey, TValue>(configuration);
            builder.SetValueSerializer(new ValueSerializer<TValue>());
            using (var producer = builder.Build()) {
                await producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = message })
                    .ConfigureAwait(false);
            }
        }
    }
}
