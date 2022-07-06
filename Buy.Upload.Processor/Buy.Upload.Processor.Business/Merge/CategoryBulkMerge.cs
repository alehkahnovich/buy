using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Buy.Upload.Processor.Business.Merge.Abstractions;
using Buy.Upload.Processor.DataAccess.Connections;
using Buy.Upload.Processor.DataAccess.Connections.Schemas;
using Buy.Upload.Processor.DataAccess.Domain;

namespace Buy.Upload.Processor.Business.Merge
{
    public sealed class CategoryBulkMerge : IBulkMerge<RawCategory> {
        private readonly IConnectionFactory _connectionFactory;

        public CategoryBulkMerge(IConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }

        public async Task MergeAsync(IEnumerable<RawCategory> parents, IEnumerable<RawCategory> siblings) {
            using (var connection = _connectionFactory.GetConnection(Schema.Content)) {
                await connection.OpenAsync().ConfigureAwait(false);

                await Bulk(connection, parents).ConfigureAwait(false);
            }
        }

        private static async Task Bulk(SqlConnection connection, IEnumerable<RawCategory> source) {
            using (var bulk = new SqlBulkCopy(connection)) {
                var table = BuildTable(new Guid("b0cd9875-fc55-4106-a56e-01e12ccac781"), source);
                bulk.DestinationTableName = $"CONTENT.{nameof(RawCategory)}";
                await bulk.WriteToServerAsync(table).ConfigureAwait(false);
            }
        }

        private static DataTable BuildTable(Guid bulkId, IEnumerable<RawCategory> source) {
            var table = new DataTable($"CONTENT.{nameof(RawCategory)}");
            table.Columns.Add(new DataColumn(nameof(RawCategory.UserId)) { DataType = typeof(Guid) });
            table.Columns.Add(new DataColumn(nameof(RawCategory.CategoryId)) { DataType = typeof(Guid) });
            table.Columns.Add(new DataColumn(nameof(RawCategory.Name)) { DataType = typeof(string) });
            table.Columns.Add(new DataColumn(nameof(RawCategory.ParentName)) { DataType = typeof(string) });
            table.Columns.Add(new DataColumn(nameof(RawCategory.ParentCategoryId)) { DataType = typeof(Guid) });
            foreach (var category in source) {
                var row = table.NewRow();
                row[nameof(RawCategory.UserId)] = bulkId;
                row[nameof(RawCategory.CategoryId)] = category.CategoryId;
                row[nameof(RawCategory.Name)] = category.Name;
                row[nameof(RawCategory.ParentName)] = category.ParentName;
                row[nameof(RawCategory.ParentCategoryId)] = !category.ParentCategoryId.HasValue ? DBNull.Value : (object)category.ParentCategoryId.Value;
                table.Rows.Add(row);
            }

            table.AcceptChanges();
            return table;
        }
    }
}