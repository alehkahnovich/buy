using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Content.Business.Providers.Abstractions;
using Buy.Content.Contract.Content;
using Buy.Content.DataAccess.Repository.Abstractions;
using Layer = Buy.Content.DataAccess.Domains;
namespace Buy.Content.Business.Providers
{
    internal sealed class CategoryProvider : ICategoryProvider {
        private readonly ICategoryRepository _repository;

        public CategoryProvider(ICategoryRepository repository) {
            _repository = repository;
        }

        public async Task<Category> GetAsync(int key) {
            var category = await _repository.GetAsync(key).ConfigureAwait(false);
            return Convert(category);
        }

        public async Task<IEnumerable<Category>> GetAsync() {
            var categories = await _repository.GetAsync().ConfigureAwait(false);
            return categories.Select(Convert);
        }

        public async Task<Category> SaveAsync(Category category) {
            var domain = Convert(category);
            await _repository.SaveAsync(domain).ConfigureAwait(false);
            return Convert(domain);
        }

        public async Task UpdateAsync(Category category) {
            await _repository.UpdateAsync(Convert(category)).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int key) {
            await _repository.DeleteAsync(key).ConfigureAwait(false);
        }

        private static Category Convert(Layer.Category domain) => 
        domain == null 
        ? null
        : new Category {
            CategoryId = domain.CategoryId,
            Name = domain.Name,
            ParentId = domain.ParentId,
            ParentName = domain.Parent?.Name,
            ModifiedDate = domain.ModifiedDate ?? domain.CreatedDate,
            WithSiblings = domain.WithSiblings
        };

        private static Layer.Category Convert(Category contract) => 
        contract == null
        ? null
        : new Layer.Category {
            CategoryId = contract.CategoryId,
            Name = contract.Name,
            ParentId = contract.ParentId
        };
    }
}