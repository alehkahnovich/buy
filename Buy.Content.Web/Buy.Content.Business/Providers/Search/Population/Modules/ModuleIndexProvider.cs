using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Content.Business.Providers.Search.Builders;
using Buy.Content.Business.Providers.Search.Population.Modules.Representation;
using Buy.Content.Business.Representation.Module;
using Buy.Content.Search.Core.Connections;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Buy.Content.Business.Providers.Search.Population.Modules
{
    internal sealed class ModuleIndexProvider : IModuleIndexProvider {
        private readonly IEngineConnection _connection;
        public ModuleIndexProvider(IEngineConnection connection) {
            _connection = connection;
        }

        public async Task<bool> IndexAsync(int id, ContentUnit content) {
            var document = JsonConvert.SerializeObject(Convert(id, content));
            var result = await _connection.GetLowLevelConnection()
                .CreateAsync<BytesResponse>(SearchConstants.Indices.Modules, $"{id}", PostData.String(document));
            return result.Success;
        }

        private static ModuleIndex Convert(int id, ContentUnit unit) {
            var attributes = unit.Attributes
                .GroupBy(entry => entry.Id)
                .ToDictionary(entry => $"{SearchConstants.AggregationPrefixes.AttributePrefix}{entry.Key}", entry => ResolveValues(entry.SelectMany(src => src.Values)).ToList());

            return new ModuleIndex {
                Id = id,
                Name = unit.Name,
                Description = unit.Description,
                Categories = new List<int> {  unit.CategoryId },
                CreatedDate = DateTime.UtcNow,
                Attributes = attributes,
                Artifacts = unit.Artifacts?.Distinct().ToList()
            };
        }

        private static IEnumerable<string> ResolveValues(IEnumerable<ContentUnitOptionValue> values) {
            foreach (var value in values) {
                if (!string.IsNullOrEmpty(value.Value)) {
                    yield return value.Value;
                    continue;
                }

                yield return $"{value.Id}";
            }
        }
    }
}