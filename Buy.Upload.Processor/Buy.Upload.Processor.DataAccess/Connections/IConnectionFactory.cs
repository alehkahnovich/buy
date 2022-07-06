using System.Data.SqlClient;
using Buy.Upload.Processor.DataAccess.Connections.Schemas;

namespace Buy.Upload.Processor.DataAccess.Connections
{
    public interface IConnectionFactory {
        SqlConnection GetConnection(Schema schema);
    }
}