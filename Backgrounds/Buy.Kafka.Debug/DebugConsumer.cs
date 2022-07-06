using Buy.Kafka.Consumers;
using Buy.Kafka.Consumers.Attributes;
using Confluent.Kafka;
using System.Threading.Tasks;

namespace Buy.Kafka.Debug
{
    [Topic("debug-topic")]
    class DebugConsumer : IConsumerProcessor<string, string> {
        public Task ConsumeAsync(ConsumeResult<string, string> message) {
            return Task.CompletedTask;
        }
    }
}
