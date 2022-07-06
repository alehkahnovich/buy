using System.Threading.Tasks;
using Buy.Content.Business.Representation.Module;

namespace Buy.Content.Business.Providers.Search.Population.Modules
{
    internal interface IModuleIndexProvider {
        Task<bool> IndexAsync(int id, ContentUnit content);
    }
}