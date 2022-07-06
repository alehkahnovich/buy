using System.Threading.Tasks;
using Buy.Kafka.Consumers;
using Buy.Kafka.Consumers.Attributes;
using Buy.Rasterization.Business.Converters.Abstractions;
using Buy.Rasterization.Contract.Messages;
using Confluent.Kafka;

namespace Buy.Rasterization.Processor.Consumers
{
    [Topic("rasterization")]
    public sealed class Rasterization : IConsumerProcessor<int, RasterizationRequest> {
        private readonly IThumbnail _thumbnail;
        public Rasterization(IThumbnail thumbnail) {
            _thumbnail = thumbnail;
        }

        public async Task ConsumeAsync(ConsumeResult<int, RasterizationRequest> message) {
            await _thumbnail.RasterizeAsync(message.Key).ConfigureAwait(false);
        }
    }
}