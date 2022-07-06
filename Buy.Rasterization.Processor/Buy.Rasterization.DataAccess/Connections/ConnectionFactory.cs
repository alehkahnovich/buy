using System;
using System.Data.SqlClient;
using Buy.Rasterization.DataAccess.Connections.Connection;
using Buy.Rasterization.DataAccess.Connections.Schemas;
using Microsoft.Extensions.Configuration;

namespace Buy.Rasterization.DataAccess.Connections
{
    internal sealed class ConnectionFactory : IConnectionFactory {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public IStoreConnection GetConnection(Schema schema) => new SqlStoreConnection(new SqlConnection(_configuration.GetConnectionString(Enum.GetName(typeof(Schema), schema))));
    }
}