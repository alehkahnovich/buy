using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;

namespace Buy.Content.Search.Core.Serialization
{
    public sealed class JsonContentBinder : ConnectionSettingsAwareSerializerBase {
        public JsonContentBinder(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings) 
            : base(builtinSerializer, connectionSettings)
        {
        }

        protected override ConnectionSettingsAwareContractResolver CreateContractResolver() => new JsonContentResolver(ConnectionSettings);
    }
}