using System;
using Buy.Content.Search.Core.Contracts.Facets;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;

namespace Buy.Content.Search.Core.Serialization
{
    public sealed class JsonContentResolver : ConnectionSettingsAwareContractResolver {
        public JsonContentResolver(IConnectionSettingsValues connectionSettings) : base(connectionSettings) {
        }

        protected override JsonConverter ResolveContractConverter(Type objectType) {
            return typeof(Facet).IsAssignableFrom(objectType) ? new PropertyJsonConverter() : base.ResolveContractConverter(objectType);
        }
    }
}