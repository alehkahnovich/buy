using Confluent.Kafka;
using System.Threading.Tasks;

namespace Buy.Kafka.Consumers
{
    public interface IConsumerProcessor<TKey, TMessage> where TMessage : class {
        Task ConsumeAsync(ConsumeResult<TKey, TMessage> message);
    }
}
