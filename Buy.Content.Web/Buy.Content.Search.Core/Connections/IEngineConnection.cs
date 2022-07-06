using Elasticsearch.Net;
using Nest;

namespace Buy.Content.Search.Core.Connections
{
    public interface IEngineConnection {
        IElasticClient GetConnection();
        IElasticLowLevelClient GetLowLevelConnection();
    }
}