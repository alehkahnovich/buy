using Buy.Content.DataAccess.Connections.Connection;
using Buy.Content.DataAccess.Connections.Schema;

namespace Buy.Content.DataAccess.Connections
{
    public interface IConnectionFactory {
        IStoreConnection GetConnection(BuyAccess access);
    }
}