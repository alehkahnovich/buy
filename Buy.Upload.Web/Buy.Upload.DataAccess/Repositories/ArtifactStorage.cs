using System.Threading.Tasks;
using Buy.Upload.DataAccess.Connections;
using Buy.Upload.DataAccess.Domains;
using Buy.Upload.DataAccess.Repositories.Abstractions;
using Buy.Upload.DataAccess.Representations.AccessType;
using Dapper;

namespace Buy.Upload.DataAccess.Repositories
{
    public sealed class ArtifactStorage : IArtifactStorage {
        private readonly IConnectionFactory _connectionFactory;

        public ArtifactStorage(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task<Artifact> GetAsync(string type, int request) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);

                return await connection.QuerySingleOrDefaultAsync<Artifact>(
                    @"select * from [CONTENT].[Artifact] 
                    where RequestId = @RequestId and [Type] = @Type",
                    new { RequestId = request, Type = type })
                .ConfigureAwait(false);
            }
        }

        public async Task<Artifact> GetAsync(int id) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.QuerySingleOrDefaultAsync<Artifact>(
                        @"select * from [CONTENT].[Artifact] 
                        where ArtifactId = @ArtifactId",
                        new { ArtifactId = id })
                    .ConfigureAwait(false);
            }
        }
    }
}