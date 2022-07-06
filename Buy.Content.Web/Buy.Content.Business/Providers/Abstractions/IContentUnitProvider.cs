using System.Threading.Tasks;
using Buy.Content.Business.Representation.Module;

namespace Buy.Content.Business.Providers.Abstractions
{
    public interface IContentUnitProvider {
        Task<ContentUnit> CreateAsync(ContentUnit content);
    }
}