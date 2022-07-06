using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Buy.Kafka.Consumers.Serialization
{
    public class ValueDeserializer<TValue> : IDeserializer<TValue> {
        public TValue Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) {
            if (isNull) return default(TValue);
            var raw = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<TValue>(raw);
        }
    }
}
