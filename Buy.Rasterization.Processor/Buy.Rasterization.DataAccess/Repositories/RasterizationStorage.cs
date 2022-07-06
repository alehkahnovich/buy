using System.Threading.Tasks;
using Buy.Rasterization.DataAccess.Connections;
using Buy.Rasterization.DataAccess.Connections.Schemas;
using Buy.Rasterization.DataAccess.Domain;
using Dapper;

namespace Buy.Rasterization.DataAccess.Repositories
{
    public sealed class RasterizationStorage : IRasterizationStorage {
        private readonly IConnectionFactory _connectionFactory;
        public RasterizationStorage(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task<RasterizationRequest> GetAsync(int id) {
            using (var connection = _connectionFactory.GetConnection(Schema.Upld)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection
                    .Pool
                    .QuerySingleOrDefaultAsync<RasterizationRequest>("select * from UPLD.Request where RequestId = @RequestId", new { RequestId = id })
                    .ConfigureAwait(false);
            }
        }
    }
}