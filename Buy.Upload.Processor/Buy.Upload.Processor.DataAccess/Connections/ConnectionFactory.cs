using System;
using System.Data.SqlClient;
using Buy.Upload.Processor.DataAccess.Connections.Schemas;
using Microsoft.Extensions.Configuration;

namespace Buy.Upload.Processor.DataAccess.Connections
{
    public sealed class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public SqlConnection GetConnection(Schema schema) => new SqlConnection(_configuration.GetConnectionString(Enum.GetName(typeof(Schema), schema)));
    }
}