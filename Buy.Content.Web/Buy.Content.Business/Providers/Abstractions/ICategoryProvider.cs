using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Content.Contract.Content;

namespace Buy.Content.Business.Providers.Abstractions
{
    public interface ICategoryProvider {
        Task<Category> GetAsync(int key);
        Task<IEnumerable<Category>> GetAsync();
        Task<Category> SaveAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int key);
    }
}