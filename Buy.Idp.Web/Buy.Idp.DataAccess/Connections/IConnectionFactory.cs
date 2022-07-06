using System.Data.SqlClient;
using Buy.Idp.DataAccess.Representations.AccessType;

namespace Buy.Idp.DataAccess.Connections
{
    public interface IConnectionFactory {
        SqlConnection GetConnection(BuyAccess access);
    }
}