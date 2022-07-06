using System.Threading.Tasks;
using Buy.Content.DataAccess.Domains;

namespace Buy.Content.DataAccess.Repository.Abstractions
{
    public interface IModuleRepository {
        Task<Module> SaveAsync(Module module);
        Task DeleteAsync(int id);
    }
}