using System.Threading.Tasks;

namespace Buy.Kafka.Consumers {
    public interface IProducer {
        Task ProduceAsync<TKey, TValue>(string topic, TKey key, TValue message) where TValue : class;
    }
}
