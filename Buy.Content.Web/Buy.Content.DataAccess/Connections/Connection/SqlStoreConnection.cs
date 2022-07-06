using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Buy.Content.DataAccess.Connections.Connection
{
    internal sealed class SqlStoreConnection : IStoreConnection {
        private readonly SqlConnection _connection;

        public SqlStoreConnection(SqlConnection connection) {
            _connection = connection;
        }

        public IDbConnection Pool => _connection;

        public async Task OpenAsync() => await OpenAsync(CancellationToken.None).ConfigureAwait(false);

        public async Task OpenAsync(CancellationToken token) => await _connection.OpenAsync(token).ConfigureAwait(false);

        public void Dispose() => _connection?.Dispose();
    }
}