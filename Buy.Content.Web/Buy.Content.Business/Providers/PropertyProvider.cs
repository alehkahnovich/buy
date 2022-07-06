using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Content.Business.Converters;
using Buy.Content.Business.Providers.Abstractions;
using Buy.Content.Contract.Content;
using Buy.Content.DataAccess.Repository.Abstractions;
using ContentAttribute = Buy.Content.Business.Representation.Content.ContentAttribute;
using Layer = Buy.Content.DataAccess.Domains;

namespace Buy.Content.Business.Providers
{
    internal sealed class PropertyProvider : IPropertyProvider {
        private readonly IPropertyRepository _repository;

        public PropertyProvider(IPropertyRepository repository) {
            _repository = repository;
        }

        public async Task<Property> GetAsync(int key) {
            var property = await _repository.GetAsync(key).ConfigureAwait(false);
            return Convert(property);
        }

        public async Task<IEnumerable<Property>> GetAsync() {
            var properties = await _repository.GetAsync().ConfigureAwait(false);
            return properties.Select(Convert);
        }

        public async Task<IEnumerable<ContentAttribute>> GetAttributesAsync(int key) {
            var properties = await _repository.GetAttributesAsync(key).ConfigureAwait(false);
            return ContentAttributeConverter.Convert(properties);
        }

        public async Task<Property> SaveAsync(Property property) {
            var domain = Convert(property);
            await _repository.SaveAsync(domain).ConfigureAwait(false);
            return Convert(domain);
        }

        public async Task UpdateAsync(Property property) {
            await _repository.UpdateAsync(Convert(property)).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int key) {
            await _repository.DeleteAsync(key).ConfigureAwait(false);
        }

        private static Property Convert(Layer.Property domain) => 
        domain == null 
        ? null
        : new Property {
            PropertyId = domain.PropertyId,
            Name = domain.Name,
            ParentId = domain.ParentId,
            ParentName = domain.ParentProperty?.Name,
            Type = domain.Type,
            IsFacet = domain.IsFacet,
            ModifiedDate = domain.ModifiedDate ?? domain.CreatedDate
        };

        private static Layer.Property Convert(Property contract) => 
        contract == null
        ? null
        : new Layer.Property {
            PropertyId = contract.PropertyId ?? default(int),
            Name = contract.Name,
            Type = contract.Type,
            IsFacet = contract.IsFacet,
            ParentId = contract.ParentId
        };
    }
}