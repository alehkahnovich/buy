using System;
using System.Data.SqlClient;
using Buy.Idp.DataAccess.Representations.AccessType;
using Microsoft.Extensions.Configuration;

namespace Buy.Idp.DataAccess.Connections
{
    internal sealed class ConnectionFactory : IConnectionFactory {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public SqlConnection GetConnection(BuyAccess access) => new SqlConnection(_configuration.GetConnectionString(Enum.GetName(typeof(BuyAccess), access)));
    }
}