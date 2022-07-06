using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buy.Content.DataAccess.Repository.Abstractions {
    public interface IRepositoryUnit<TKey, TUnit> {
        Task<IEnumerable<TUnit>> GetAsync();
        Task<TUnit> GetAsync(TKey id);
        Task<TKey> SaveAsync(TUnit property);
        Task UpdateAsync(TUnit property);
        Task DeleteAsync(TKey id);
    }
}