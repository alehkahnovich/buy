using Buy.Rasterization.DataAccess.Connections.Connection;
using Buy.Rasterization.DataAccess.Connections.Schemas;

namespace Buy.Rasterization.DataAccess.Connections
{
    public interface IConnectionFactory {
        IStoreConnection GetConnection(Schema schema);
    }
}