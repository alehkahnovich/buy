using System;
using System.Data.SqlClient;
using Buy.Content.DataAccess.Connections.Connection;
using Buy.Content.DataAccess.Connections.Schema;
using Microsoft.Extensions.Configuration;

namespace Buy.Content.DataAccess.Connections
{
    internal sealed class ConnectionFactory : IConnectionFactory {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public IStoreConnection GetConnection(BuyAccess access) => new SqlStoreConnection(new SqlConnection(_configuration.GetConnectionString(Enum.GetName(typeof(BuyAccess), access))));
    }
}