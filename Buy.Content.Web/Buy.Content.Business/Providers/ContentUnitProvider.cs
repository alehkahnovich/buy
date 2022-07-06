using System;
using System.Threading.Tasks;
using Buy.Content.Business.Converters;
using Buy.Content.Business.Providers.Abstractions;
using Buy.Content.Business.Providers.Search.Population.Modules;
using Buy.Content.Business.Representation.Module;
using Buy.Content.DataAccess.Repository.Abstractions;

namespace Buy.Content.Business.Providers
{
    internal sealed class ContentUnitProvider : IContentUnitProvider {
        private readonly IModuleRepository _repository;
        private readonly IModuleIndexProvider _searchIndex;

        public ContentUnitProvider(IModuleRepository repository, IModuleIndexProvider searchIndex) {
            _repository = repository;
            _searchIndex = searchIndex;
        }

        public async Task<ContentUnit> CreateAsync(ContentUnit content) {
            var module = ContentUnitConverter.Convert(content);
            var result = await _repository.SaveAsync(module).ConfigureAwait(false);
            var submitted = await _searchIndex.IndexAsync(result.ModuleId, content).ConfigureAwait(false);

            if (submitted)
                return ContentUnitConverter.Convert(result);

            await _repository.DeleteAsync(result.ModuleId).ConfigureAwait(false);
            throw new ArgumentException("Search index unavailable", nameof(content));
        }
    }
}