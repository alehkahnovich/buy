using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

namespace Buy.Kafka.Consumers.Serialization
{
    public class ValueSerializer<TValue> : ISerializer<TValue> {
        public byte[] Serialize(TValue data, SerializationContext context) {
            var json = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
