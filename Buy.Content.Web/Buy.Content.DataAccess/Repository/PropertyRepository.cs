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
    internal sealed class PropertyRepository : IPropertyRepository {
        private readonly IConnectionFactory _connection;

        private const string Select = @"select source.*, sibling.WithSiblings, parent.* from [CONTENT].Property as source
        left join [Content].Property as parent 
        on source.ParentId = parent.PropertyId
        left join (select ParentId, CAST(1 as bit) as WithSiblings from [CONTENT].[Property]
        where ParentId is not null
        group by ParentId) as sibling
        on sibling.ParentId = source.PropertyId";

        public PropertyRepository(IConnectionFactory connection) {
            _connection = connection;
        }

        public async Task<IEnumerable<Property>> GetAsync() {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.Pool.QueryAsync<Property, Property, Property>(Select,
                        (source, parent) => {
                            source.ParentProperty = parent;
                            return source;
                        },
                        splitOn: nameof(Property.PropertyId))
                    .ConfigureAwait(false);
            }
        }

        public async Task<Property> GetAsync(int id) {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                var results = await connection.Pool.QueryAsync<Property, Property, Property>($"{Select} where source.PropertyId = @PropertyId",
                        (source, parent) => {
                            source.ParentProperty = parent;
                            return source;
                        },
                        splitOn: nameof(Property.PropertyId),
                        param: new { PropertyId = id })
                    .ConfigureAwait(false);

                return results.SingleOrDefault();
            }
        }

        public async Task<int> SaveAsync(Property property) {
            var domain = property.WithTimeStamp();
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                var identity = await connection
                    .Pool
                    .QuerySingleAsync<int>(@"insert into [CONTENT].Property (Name, Type, ParentId, IsFacet) values (@Name, @Type, @ParentId, @IsFacet);
                        select cast(SCOPE_IDENTITY() as int)",
                        domain).ConfigureAwait(false);

                domain.PropertyId = identity;
                return identity;
            }
        }

        public async Task UpdateAsync(Property property) {
            if (property.PropertyId == default(int))
                throw new ArgumentException("Property primary key is null, not applicable for update operation");
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                await connection
                    .Pool
                    .ExecuteAsync(
                        "update [CONTENT].Property set Name = @Name, ParentId = @ParentId, IsFacet = @IsFacet, Type = @Type, ModifiedDate = @ModifiedDate where PropertyId = @PropertyId",
                        property.WithTimeStamp()).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id) {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                if (await ExistsAsParentAsync(connection, id).ConfigureAwait(false))
                    throw new ArgumentException($"Property with {id} is set as parent and could not be deleted", nameof(id));

                await connection
                    .Pool
                    .ExecuteAsync(
                        "delete from [CONTENT].Property where PropertyId = @Property",
                        new { Property = id }).ConfigureAwait(false);
            }
        }

        private static async Task<bool> ExistsAsParentAsync(IStoreConnection connection, int id) {
            var results =
                await connection.Pool.ExecuteScalarAsync<int>(
                    "select 1 from [CONTENT].Property where ParentId = @ParentId", new {
                        ParentId = id
                    });
            return results > 0;
        }

        public async Task<IEnumerable<Property>> GetAttributesAsync(int id) {
            using (var connection = _connection.GetConnection(BuyAccess.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);
                return await connection.Pool.QueryAsync<Property>(@"
                        select prop.*, behavior.[Type] as Behavior, cntrl.[Type] as [Control]
                        from [Content].CategoryProperty as catprop
                        inner join [Content].Property as prop
                        left join [Content].Behavior as behavior
                        on behavior.BehaviorId = prop.BehaviorId
                        left join [Content].[Control] as cntrl
                        on cntrl.ControlId = prop.ControlId
                        on prop.PropertyId = catprop.PropertyId
                        where catprop.CategoryId = @CategoryId
                        order by prop.SortOrder",
                        new { CategoryId = id })
                    .ConfigureAwait(false);
            }
        }
    }
}