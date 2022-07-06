using System;
using System.Collections.Generic;
using System.Linq;
using Buy.Content.Contract.Search.Facets.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Buy.Content.Contract.Search.Facets.Converters
{
    internal class FacetConverter : JsonConverter {
        private static readonly JsonSerializerSettings Supress = new JsonSerializerSettings {
            ContractResolver = new FacetContractResolver()
        };
        private static readonly string Discriptor = nameof(Facet.Type).ToLowerInvariant();
        private static readonly IDictionary<string, FacetType> Discriptors = Enum.GetValues(typeof(FacetType)).Cast<FacetType>()
                .Select(entry => new { Enum = entry, Description = entry.GetEnumDescription() })
                .ToDictionary(entry => entry.Description, entry => entry.Enum, StringComparer.OrdinalIgnoreCase);
        public override bool CanConvert(Type objectType) => typeof(Facet).IsAssignableFrom(objectType);
        public override bool CanWrite => false;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var @object = JObject.Load(reader);
            if (!@object.ContainsKey(Discriptor)) return null;
            var discriptor = @object[Discriptor].Value<string>();
            if (!Discriptors.ContainsKey(discriptor)) return null;
            switch (Discriptors[discriptor]) {
                case FacetType.Text:
                    return JsonConvert.DeserializeObject<FacetText>(@object.ToString(), Supress);
                case FacetType.Date:
                    return JsonConvert.DeserializeObject<FacetDate>(@object.ToString(), Supress);
                case FacetType.NumberRange:
                    return JsonConvert.DeserializeObject<FacetRangeNumber>(@object.ToString(), Supress);
                case FacetType.Number:
                    return JsonConvert.DeserializeObject<FacetNumber>(@object.ToString(), Supress);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}
