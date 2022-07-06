using System;
using System.Linq;
using Buy.Content.Search.Core.Serialization;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using Serilog;

namespace Buy.Content.Search.Core.Connections
{
    public sealed class EngineConnection : IEngineConnection {
        private const string ConnectionString = "SearchConnectionStrings";
        private readonly IConfiguration _configuration;
        private readonly Lazy<IElasticClient> _initializer;
        private readonly Lazy<IElasticLowLevelClient> _low;
        private readonly ILogger _logger;

        public EngineConnection(IConfiguration configuration, ILogger logger) {
            _configuration = configuration;
            _logger = logger;
            _initializer = new Lazy<IElasticClient>(BuildConnection);
            _low = new Lazy<IElasticLowLevelClient>(BuildLoweLevelConnection);
        }

        public IElasticClient GetConnection() => _initializer.Value;
        public IElasticLowLevelClient GetLowLevelConnection() => _low.Value;

        private IElasticClient BuildConnection() {
            var connection = new ConnectionString();
            _configuration.GetSection(ConnectionString).Bind(connection);
            _logger.Information($"Elastic-Connection: {string.Join(',', connection.Uri)}");
            var pool = new SniffingConnectionPool(connection.Uri?.Select(entry => new Uri(entry)));
            var settings = new ConnectionSettings(pool, (builtin, config) => 
                new JsonContentBinder(builtin, config));
            return new ElasticClient(settings);
        }

        private IElasticLowLevelClient BuildLoweLevelConnection() {
            var connection = new ConnectionString();
            _configuration.GetSection(ConnectionString).Bind(connection);
            var pool = new SniffingConnectionPool(connection.Uri?.Select(entry => new Uri(entry)));
            var settings = new ConnectionSettings(pool, (builtin, config) =>
                new JsonContentBinder(builtin, config));
            return new ElasticLowLevelClient(settings);
        }
    }
}