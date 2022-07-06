using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Buy.Content.DataAccess.Connections;
using Buy.Content.DataAccess.Connections.Connection;
using Buy.Content.DataAccess.Connections.Schema;
using Buy.Content.DataAccess.Domains;
using Buy.Content.DataAccess.Domains.Extensions;
using Buy.Content.DataAccess.Repository.Abstractions;
using Dapper;
using Serilog;

namespace Buy.Content.DataAccess.Repository
{
    public sealed class ModuleRepository : IModuleRepository {
        private static readonly Guid TmpUserKey = new Guid("B0CD9875-FC55-4106-A56E-01E12CCAC782");//todo:
        private readonly IConnectionFactory _factory;
        private readonly ILogger _logger;
        public ModuleRepository(IConnectionFactory factory, ILogger logger) {
            _factory = factory;
            _logger = logger;
        }

        public async Task<Module> SaveAsync(Module module) {
            module.CreatedBy = TmpUserKey;
            var domain = module.WithTimeStamp();
            using (var connection = _factory.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await TransactionalInsertAsync(connection, domain).ConfigureAwait(false);
            }
        }

        private async Task<Module> TransactionalInsertAsync(IStoreConnection connection, Module module) {
            var transaction = connection.Pool.BeginTransaction();
            try
            {
                var inserted = await CreateModuleAsync(transaction, connection.Pool, module).ConfigureAwait(false);
                await CreatePropertiesAsync(transaction, connection.Pool, inserted).ConfigureAwait(false);
                await CreateArtifactsAsync(transaction, connection.Pool, inserted).ConfigureAwait(false);
                transaction.Commit();
                return inserted;
            }
            catch(Exception exception) { transaction.Rollback(); _logger.Error(exception, string.Empty); throw; }
            finally { transaction.Dispose(); }
        }

        private static async Task<Module> CreateModuleAsync(IDbTransaction transaction, IDbConnection connection, Module module) {
            var identity = await connection.QuerySingleAsync<int>(
                @"insert into [CONTENT].Module 
                (CategoryId, Name, Description, CreatedBy, CreatedDate, ModifiedDate)
                values 
                (@CategoryId, @Name, @Description, @CreatedBy, @CreatedDate, @ModifiedDate);
                select cast(SCOPE_IDENTITY() as int)", module, transaction)
            .ConfigureAwait(false);

            module.ModuleId = identity;
            return module;
        }

        private static async Task CreatePropertiesAsync(IDbTransaction transaction, IDbConnection connection, Module module) {
            await connection.ExecuteAsync(
                @"insert into [CONTENT].ModuleProperty (ModuleId, PropertyId, Value) values (@ModuleId, @PropertyId, @Value)",
                module.Properties.Select(property => new ModuleProperty {
                    ModuleId = module.ModuleId,
                    PropertyId = property.PropertyId,
                    Value = property.Value
                }), transaction).ConfigureAwait(false);
        }

        private static async Task CreateArtifactsAsync(IDbTransaction transaction, IDbConnection connection, Module module) {
            await connection.ExecuteAsync(
                @"insert into [CONTENT].ModuleArtifact (ModuleId, ArtifactId) values (@ModuleId, @ArtifactId)",
                module.Artifacts.Select(artifact => new {
                    module.ModuleId,
                    ArtifactId = artifact
                }), transaction).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id) {
            using (var connection = _factory.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                await connection
                    .Pool
                    .ExecuteAsync(
                        "delete from [CONTENT].Module where ModuleId = @ModuleId",
                        new { ModuleId = id }).ConfigureAwait(false);
            }
        }
    }
}