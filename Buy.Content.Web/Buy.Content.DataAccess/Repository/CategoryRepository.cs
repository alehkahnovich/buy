using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Content.DataAccess.Connections;
using Buy.Content.DataAccess.Connections.Connection;
using Buy.Content.DataAccess.Connections.Schema;
using Buy.Content.DataAccess.Domains;
using Buy.Content.DataAccess.Domains.Extensions;
using Buy.Content.DataAccess.Repository.Abstractions;
using Dapper;

namespace Buy.Content.DataAccess.Repository
{
    internal sealed class CategoryRepository : ICategoryRepository {
        private readonly IConnectionFactory _connection;

        private const string Select = @"select source.*, sibling.WithSiblings, parent.* from [CONTENT].Category as source
        left join [Content].Category as parent 
        on source.ParentId = parent.CategoryId
        left join (select ParentId, CAST(1 as bit) as WithSiblings from [CONTENT].[Category]
        where ParentId is not null
        group by ParentId) as sibling
        on sibling.ParentId = source.CategoryId";

        public CategoryRepository(IConnectionFactory connection) {
            _connection = connection;
        }

        public async Task<IEnumerable<Category>> GetAsync() {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.Pool.QueryAsync<Category, Category, Category>(Select, (source, parent) => {
                    source.Parent = parent;
                    return source;
                }, splitOn: nameof(Category.CategoryId)).ConfigureAwait(false);
            }
        }

        public async Task<Category> GetAsync(int id) {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await GetAsync(connection, id).ConfigureAwait(false);
            }
        }

        private static async Task<Category> GetAsync(IStoreConnection connection, int id) {
            var results = await connection.Pool.QueryAsync<Category, Category, Category>(
                $"{Select} where source.CategoryId = @CategoryId", (source, parent) => {
                    source.Parent = parent;
                    return source;
                },
                splitOn: nameof(Category.CategoryId),
                param: new { CategoryId = id }).ConfigureAwait(false);
            return results.SingleOrDefault();
        }

        public async Task<int> SaveAsync(Category category) {
            if (category.CategoryId.HasValue)
                throw new ArgumentException("Category contains primary key, not applicable for insert operation");
            var domain = category.WithTimeStamp();
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                var identity = await connection
                    .Pool
                    .QuerySingleAsync<int>(@"insert into [CONTENT].Category (Name, ParentId, CreatedDate) values (@Name, @ParentId, @CreatedDate);
                        select cast(SCOPE_IDENTITY() as int)",
                        domain).ConfigureAwait(false);

                category.CategoryId = identity;
                return identity;
            }
        }

        public async Task UpdateAsync(Category category) {
            if (!category.CategoryId.HasValue)
                throw new ArgumentException("Category primary key is null, not applicable for update operation");

            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                await connection
                    .Pool
                    .ExecuteAsync(
                        "update [CONTENT].Category set Name = @Name, ParentId = @ParentId, ModifiedDate = @ModifiedDate where CategoryId = @CategoryId",
                        category.WithTimeStamp()).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id) {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                if (await ExistsAsParentAsync(connection, id).ConfigureAwait(false))
                    throw new ArgumentException($"Category with {id} is set as parent and could not be deleted", nameof(id));

                await connection
                    .Pool
                    .ExecuteAsync(
                        "delete from [CONTENT].Category where CategoryId = @CategoryId",
                        new { CategoryId = id }).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Category>> GetLightweightAsync() {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection
                    .Pool
                    .QueryAsync<Category>("select CategoryId, ParentId, Name from [CONTENT].Category").ConfigureAwait(false);
            }
        }

        private static async Task<bool> ExistsAsParentAsync(IStoreConnection connection, int id) {
            var results =
                await connection.Pool.ExecuteScalarAsync<int>(
                    "select 1 from [CONTENT].Category where ParentId = @ParentId", new {
                        ParentId = id
                    });
            return results > 0;
        }
    }
}