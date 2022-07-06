using System.Threading.Tasks;
using Buy.Rasterization.DataAccess.Connections;
using Buy.Rasterization.DataAccess.Connections.Schemas;
using Buy.Rasterization.DataAccess.Domain;
using Dapper;

namespace Buy.Rasterization.DataAccess.Repositories
{
    public sealed class ArtifactStorage : IArtifactStorage {
        private readonly IConnectionFactory _connectionFactory;

        public ArtifactStorage(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task<Artifact> SaveAsync(Artifact artifact) {
            using (var connection = _connectionFactory.GetConnection(Schema.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                artifact.ArtifactId = await connection
                    .Pool
                    .QuerySingleAsync<int>(@"insert into [CONTENT].Artifact 
                        (RequestId, Type, UploadKey, SortOrder) 
                        values (@RequestId, @Type, @UploadKey, @SortOrder); 
                        select cast(SCOPE_IDENTITY() as int)", artifact)
                    .ConfigureAwait(false);
                return artifact;
            }
        }
    }
}