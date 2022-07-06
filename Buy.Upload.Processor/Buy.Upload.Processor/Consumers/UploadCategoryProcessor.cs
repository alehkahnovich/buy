using System.Threading.Tasks;
using Buy.Kafka.Consumers;
using Buy.Kafka.Consumers.Attributes;
using Buy.Upload.Processor.Business.Processors.Abstractions;
using Buy.Upload.Processor.Messages;
using Confluent.Kafka;

namespace Buy.Upload.Processor.Consumers
{
    [Topic("upload-category-complete")]
    public class UploadCategoryProcessor : IConsumerProcessor<string, UploadCategoryMessage> {
        private readonly ICategoryProcessor _processor;

        public UploadCategoryProcessor(ICategoryProcessor processor) {
            _processor = processor;
        }

        public async Task ConsumeAsync(ConsumeResult<string, UploadCategoryMessage> message) {
            await _processor.ProcessAsync(message.Value).ConfigureAwait(false);
        }
    }
}