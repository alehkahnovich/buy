using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Content.DataAccess.Connections;
using Buy.Content.DataAccess.Connections.Schema;
using Buy.Content.DataAccess.Domains.DTO;
using Buy.Content.DataAccess.Repository.Abstractions;
using Dapper;

namespace Buy.Content.DataAccess.Repository
{
    internal sealed class FacetRepository : IFacetRepository {
        private readonly IConnectionFactory _connection;
        private const string Type = "string";
        public FacetRepository(IConnectionFactory connection) => _connection = connection;

        public async Task<IEnumerable<Agg>> GetAttributesAsync(string prefix, int cat) {
            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException(nameof(prefix));

            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.Pool.QueryAsync<Agg>(
                    @"select (concat(@Prefix, [RawKey])) as [AggKey], [RawKey], [Type], [Name] 
                    from (select cast(prop.PropertyId as nvarchar(255)) as [RawKey], [Type], [Name] from [CONTENT].Property as prop
                    inner join [CONTENT].CategoryProperty as cat
                    on cat.PropertyId = prop.PropertyId 
                    where prop.IsFacet = 1 and cat.CategoryId = @CategoryId) 
                    as facets", new { Prefix = prefix, CategoryId = cat })
                .ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Agg>> GetCategoriesAsync() {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.Pool.QueryAsync<Agg>(
                        @"select [RawKey] as [AggKey], RootAggKey, [RawKey], [Type], [Name] 
                        from (select cast(CategoryId as nvarchar(255)) as [RawKey], cast(ParentId as nvarchar(255)) as [RootAggKey], @Type as [Type], [Name] from [CONTENT].Category) as facets", new { Type })
                    .ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Map>> GetMapAsync(int cat) {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.Pool.QueryAsync<Map>(
                    @"select [Key], [Type], [Name] 
                    from (select cast(prop.PropertyId as nvarchar(255)) as [Key], [Type], [Name] from [CONTENT].Property as prop 
                    inner join [CONTENT].CategoryProperty as cat
                    on cat.PropertyId = prop.PropertyId
                    where prop.IsFacet = 0 and cat.CategoryId = @CategoryId) as facets", new { CategoryId = cat })
                .ConfigureAwait(false);
            }
        }
    }
}