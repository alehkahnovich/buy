using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Idp.DataAccess.Connections;
using Buy.Idp.DataAccess.Domain;
using Buy.Idp.DataAccess.Representations.AccessType;
using Dapper;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Buy.Idp.Business.Stores
{
    public sealed class PersistedGrantStore : IPersistedGrantStore {
        private readonly IConnectionFactory _connectionFactory;

        public PersistedGrantStore(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task StoreAsync(PersistedGrant grant) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                await connection
                    .ExecuteAsync("insert into IDP.[Grant] ([Key], ClientId, UserId, Type, CreationTime, Expiration, Data) values (@Key, @ClientId, @UserId, @Type, @CreationTime, @Expiration, @Data)", Convert(grant))
                    .ConfigureAwait(false);
            }
        }

        public async Task<PersistedGrant> GetAsync(string key) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                var grant = await connection.QuerySingleAsync<Grant>("select * from IDP.[Grant] where [Key] = @Key",
                    new { Key = key }).ConfigureAwait(false);
                return Convert(grant);
            }
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                var grant = await connection.QueryAsync<Grant>("select * from IDP.[Grant] where UserId = @SubjectId",
                    new { SubjectId = subjectId }).ConfigureAwait(false);
                return grant.Select(Convert);
            }
        }

        public async Task RemoveAsync(string key) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                await connection
                    .ExecuteAsync("delete from IDP.[Grant] where [Key] = @Key", new { Key = key })
                    .ConfigureAwait(false);
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                await connection
                    .ExecuteAsync("delete from IDP.[Grant] where UserId = @UserId and ClientId = @ClientId", new { UserId = subjectId, ClientId = clientId })
                    .ConfigureAwait(false);
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type) {
            using (var connection = _connectionFactory.GetConnection(BuyAccess.Idp)) {
                await connection.OpenAsync().ConfigureAwait(false);
                await connection
                    .ExecuteAsync("delete from IDP.[Grant] where UserId = @UserId and ClientId = @ClientId and Type = @Type", new { UserId = subjectId, ClientId = clientId, Type = type })
                    .ConfigureAwait(false);
            }
        }

        private static Grant Convert(PersistedGrant grant) => 
            new Grant {
                Key = grant.Key,
                ClientId = Guid.Parse(grant.ClientId),
                UserId = Guid.Parse(grant.SubjectId),
                Type = grant.Type,
                CreationTime = grant.CreationTime,
                Expiration = grant.Expiration,
                Data = grant.Data
            };

        private static PersistedGrant Convert(Grant grant) =>
            new PersistedGrant {
                Key = grant.Key,
                ClientId = grant.ClientId.ToString(),
                SubjectId = grant.UserId.ToString(),
                Type = grant.Type,
                CreationTime = grant.CreationTime,
                Expiration = grant.Expiration,
                Data = grant.Data
            };
    }
}