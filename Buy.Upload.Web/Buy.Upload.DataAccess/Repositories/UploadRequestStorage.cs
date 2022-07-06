using System.Threading.Tasks;
using Buy.Upload.DataAccess.Connections;
using Buy.Upload.DataAccess.Domains;
using Buy.Upload.DataAccess.Repositories.Abstractions;
using Buy.Upload.DataAccess.Representations.AccessType;
using Dapper;

namespace Buy.Upload.DataAccess.Repositories
{
    internal sealed class UploadRequestStorage : IUploadRequestStorage {
        private readonly IConnectionFactory _connectionFactory;

        public UploadRequestStorage(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task<Request> SaveAsync(Request request) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Upld)) {
                await connection.OpenAsync().ConfigureAwait(false);
                request.RequestId = await connection
                    .QuerySingleAsync<int>(@"insert into UPLD.Request (UploadKey, RawName)
                                    values (@UploadKey, @RawName);
                                    select cast(SCOPE_IDENTITY() as int)", request)
                    .ConfigureAwait(false);
                return request;
            }
        }

        public async Task<Request> GetAsync(int key) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Upld)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection
                    .QuerySingleOrDefaultAsync<Request>("select * from UPLD.Request where RequestId = @RequestId",
                        new { RequestId = key })
                        .ConfigureAwait(false);
            }
        }

        public async Task<bool> ExistsAsync(int key) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Upld)) {
                await connection.OpenAsync().ConfigureAwait(false);
                var count = await connection
                    .ExecuteScalarAsync<int>("select 1 from UPLD.Request where RequestId = @RequestId",
                        new { RequestId = key })
                    .ConfigureAwait(false);
                return count > 0;
            }
        }
    }
}