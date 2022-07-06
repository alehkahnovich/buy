using System;
using System.Collections.Generic;
using System.Linq;
using Buy.Content.Search.Core.Contracts.Facets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Buy.Content.Search.Core.Serialization
{
    public sealed class PropertyJsonConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => JToken.FromObject(value).WriteTo(writer);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var json = JObject.Load(reader);
            var properties = new List<Property>();
            foreach (var token in json) {
                var property = Resolve(token.Key, token.Value);
                if (property == null) continue;
                properties.Add(property);
            }

            return new Facet {
                Properties = properties
            };
        }

        private static Property Resolve(string key, JToken token) {
            switch (token.Type) {
                case JTokenType.Array:
                    return ResolveConcrete(key, token.First, token.Values());
                default:
                    return ResolveConcrete(key, token);
            }
        }

        private static Property ResolveConcrete(string key, JToken token, IEnumerable<JToken> values = null) {
            Property property;
            switch (token.Type) {
                case JTokenType.Date:
                    property = new Date {
                        Id = key,
                        Value = values?.Select(value => value.ToObject<DateTime?>()) ?? new List<DateTime?> { token.ToObject<DateTime?>() }
                    };
                    break;
                case JTokenType.Float:
                case JTokenType.Integer:
                    property = new Number {
                        Id = key,
                        Value = values?.Select(value => value.ToObject<double?>()) ?? new List<double?> { token.ToObject<double?>() }
                    };
                    break;
                default:
                    property = new Text {
                        Id = key,
                        Value = values?.Select(value => value.ToObject<string>()) ?? new List<string> { token.ToObject<string>() }
                    };
                    break;
            }

            return property;
        }

        public override bool CanConvert(Type objectType) => typeof(Property).IsAssignableFrom(objectType);
    }
}