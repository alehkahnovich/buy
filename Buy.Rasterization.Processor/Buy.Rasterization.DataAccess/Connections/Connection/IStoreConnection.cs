using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Buy.Rasterization.DataAccess.Connections.Connection
{
    public interface IStoreConnection : IDisposable {
        IDbConnection Pool { get; }
        Task OpenAsync();
        Task OpenAsync(CancellationToken token);
    }
}