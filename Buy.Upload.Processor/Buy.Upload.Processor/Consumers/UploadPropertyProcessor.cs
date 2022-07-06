using System.Threading.Tasks;
using Buy.Kafka.Consumers;
using Buy.Kafka.Consumers.Attributes;
using Buy.Upload.Processor.Messages;
using Confluent.Kafka;

namespace Buy.Upload.Processor.Consumers
{
    [Topic("upload-property-complete")]
    public sealed class UploadPropertyProcessor : IConsumerProcessor<string, UploadPropertyMessage> {
        public Task ConsumeAsync(ConsumeResult<string, UploadPropertyMessage> message) {
            return Task.CompletedTask;
        }
    }
}