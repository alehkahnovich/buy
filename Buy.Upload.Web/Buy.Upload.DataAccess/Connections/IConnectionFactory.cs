using System.Data.SqlClient;
using Buy.Upload.DataAccess.Representations.AccessType;

namespace Buy.Upload.DataAccess.Connections
{
    public interface IConnectionFactory {
        SqlConnection GetConnection(BuyAccess access);
    }
}