using System.Threading.Tasks;
using Buy.Upload.Processor.DataAccess.Connections;
using Buy.Upload.Processor.DataAccess.Connections.Schemas;
using Buy.Upload.Processor.DataAccess.Domain;
using Buy.Upload.Processor.DataAccess.Repositories.Abstractions;
using Dapper;

namespace Buy.Upload.Processor.DataAccess.Repositories
{
    public sealed class UploadRequestRepository : IUploadRequestRepository {
        private readonly IConnectionFactory _connections;
        public UploadRequestRepository(IConnectionFactory connections) {
            _connections = connections;
        }

        public async Task<UploadRequest> GetAsync(string key) {
            using (var connection = _connections.GetConnection(Schema.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection
                    .QuerySingleOrDefaultAsync<UploadRequest>("select * from CONTENT.UploadRequest where UploadRequestId = @UploadRequestId",
                        new { UploadRequestId = key })
                    .ConfigureAwait(false);
            }
        }
    }
}