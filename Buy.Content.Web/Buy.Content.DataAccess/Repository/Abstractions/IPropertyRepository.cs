using System.Collections.Generic;
using System.Threading.Tasks;
using Buy.Content.DataAccess.Domains;

namespace Buy.Content.DataAccess.Repository.Abstractions
{
    public interface IPropertyRepository : IRepositoryUnit<int, Property> {
        Task<IEnumerable<Property>> GetAttributesAsync(int id);
    }
}