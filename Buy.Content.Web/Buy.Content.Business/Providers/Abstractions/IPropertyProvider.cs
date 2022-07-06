using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Content.Contract.Content;
using ContentAttribute = Buy.Content.Business.Representation.Content.ContentAttribute;

namespace Buy.Content.Business.Providers.Abstractions
{
    public interface IPropertyProvider {
        Task<Property> GetAsync(int key);
        Task<IEnumerable<Property>> GetAsync();
        Task<IEnumerable<ContentAttribute>> GetAttributesAsync(int key);
        Task<Property> SaveAsync(Property category);
        Task UpdateAsync(Property category);
        Task DeleteAsync(int key);
    }
}