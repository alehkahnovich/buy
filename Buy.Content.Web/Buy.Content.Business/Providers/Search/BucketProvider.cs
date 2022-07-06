using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Content.Business.Providers.Search.Builders;
using Buy.Content.Business.Representation.Buckets;
using Buy.Content.Business.Representation.Search;
using Buy.Content.DataAccess.Domains.DTO;
using Buy.Content.DataAccess.Repository.Abstractions;
using Buy.Content.Search.Core.Connections;
using Buy.Content.Search.Core.Contracts;
using Nest;

namespace Buy.Content.Business.Providers.Search
{
    internal sealed class BucketProvider : IBucketProvider {
        private const long DocCountThreshold = 0;
        private readonly IEngineConnection _connection;
        private readonly IFacetRepository _facets;
        private readonly IAggregationBuilder _aggregation;
        public BucketProvider(IEngineConnection connection, IFacetRepository facets, IAggregationBuilder aggregation) {
            _connection = connection;
            _facets = facets;
            _aggregation = aggregation;
        }

        public async Task<IEnumerable<Category>> GetAsync() {
            var connection = _connection.GetConnection();
            var aggregations = _aggregation.Build();

            var result = await connection.SearchAsync<Module>(aggregations)
                .ConfigureAwait(false);

            var terms = ResolveBuckets(result.Aggregations.Terms(SearchConstants.AggregationConstants.Categories))
                .ToDictionary(entry => entry.Key, entry => entry);

            var facets = (await _facets.GetCategoriesAsync().ConfigureAwait(false)).ToList();

            return BuildHierarchy(facets, terms).ToList();
        }

        private static IEnumerable<Category> BuildHierarchy(IEnumerable<Agg> agg, IDictionary<string, NamedBucket> terms) {
            var hierarchy = new AggHierarchy(agg);

            var roots = hierarchy.GetRoots();

            var leafs = hierarchy.GeLeafs();

            foreach (var root in roots)
                yield return Convert(root.Value, leafs, terms);
        }

        private static Category Convert(Agg root, IDictionary<string, IEnumerable<Agg>> leafs, IDictionary<string, NamedBucket> categories) {
            var category = new Category { Id = root.AggKey, Name = root.Name };

            if (categories.ContainsKey(root.AggKey)) {
                var current = categories[root.AggKey];
                category.Count = current.Count ?? DocCountThreshold;
            }

            category.Siblings = leafs.ContainsKey(root.AggKey)
                ? leafs[root.AggKey].Select(entry => Convert(entry, leafs, categories))
                : Enumerable.Empty<Category>();

            return category;
        }


        private static IEnumerable<NamedBucket> ResolveBuckets(TermsAggregate<string> terms) {
            if (terms?.Buckets == null)
                yield break;

            foreach (var bucket in terms.Buckets) {
                yield return new NamedBucket {
                    Key = bucket.Key,
                    Count = bucket.DocCount
                };
            }
        }
    }
}