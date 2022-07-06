using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buy.Content.Business.Providers.Search.Builders;
using Buy.Content.Business.Representation.Search;
using Buy.Content.DataAccess.Domains.DTO;
using Buy.Content.DataAccess.Repository.Abstractions;
using Buy.Content.Search.Core.Connections;
using Buy.Content.Search.Core.Contracts;
using Nest;

namespace Buy.Content.Business.Providers.Search
{
    internal sealed class SearchProvider : ISearchProvider {
        private readonly IEngineConnection _connection;
        private readonly ISearchQueryBuilder _query;
        private readonly IFacetRepository _facets;
        public SearchProvider(IEngineConnection connection, ISearchQueryBuilder query, IFacetRepository facets) {
            _connection = connection;
            _query = query;
            _facets = facets;
        }

        public async Task<SearchQueryResult<Module>> SearchAsync(SearchQuery query) {

            if (!query.Category.HasValue)
                return await SearchSuppressAggregations(query).ConfigureAwait(false);

            var connection = _connection.GetConnection();

            var agg = (await _facets.GetAttributesAsync(SearchConstants.AggregationPrefixes.AttributePrefix, query.Category.Value).ConfigureAwait(false))
                .ToDictionary(entry => entry.AggKey, entry => entry);

            var request = _query.Build(query, agg.Values);

            var result = await connection.SearchAsync<Module>(request)
                .ConfigureAwait(false);

            if (!result.IsValid)
                throw new ArgumentException(result.ApiCall?.DebugInformation, nameof(query));

            var map = (await _facets.GetMapAsync(query.Category.Value).ConfigureAwait(false))
                .ToDictionary(entry => entry.Key, entry => entry);

            var facets = ResolveGlobalFacets(result.Aggregations
                .Children(SearchConstants.AggregationConstants.GlobalAttributes), agg, map);

            return new SearchQueryResult<Module> {
                Total = result.Total,
                Results = result.Documents,
                Facets = facets
            };
        }

        private async Task<SearchQueryResult<Module>> SearchSuppressAggregations(SearchQuery query) {
            var request = _query.Build(query);
            var connection = _connection.GetConnection();
            var result = await connection.SearchAsync<Module>(request)
                .ConfigureAwait(false);

            return new SearchQueryResult<Module> {
                Total = result.Total,
                Results = result.Documents
            };
        }

        private static IEnumerable<FacetContainer> ResolveGlobalFacets(SingleBucketAggregate attributes, IDictionary<string, Agg> agg, IDictionary<string, Map> map) {
            if (attributes == null) return Enumerable.Empty<FacetContainer>();
            var containers = new List<FacetContainer>();
            foreach (var (_, value) in attributes) 
                containers.AddRange(ResolveFacets((SingleBucketAggregate) value, agg, map));

            return containers;
        }

        private static IEnumerable<FacetContainer> ResolveFacets(SingleBucketAggregate attributes, IDictionary<string, Agg> agg, IDictionary<string, Map> map) {
            if (attributes == null) return Enumerable.Empty<FacetContainer>();
            var containers = new List<FacetContainer>();

             foreach (var (key, value) in attributes) {
                if (!agg.ContainsKey(key)) continue;
                var aggregation = agg[key];

                var container = new FacetContainer {
                    Key = aggregation.RawKey,
                    Name = aggregation.Name
                };

                var facets = new List<Facet>();

                switch (value) {
                    case BucketAggregate buckets:
                        facets.AddRange(ResolveBuckets(buckets, map));
                        break;
                    case StatsAggregate stats:
                        facets.Add(ResolveStats(stats));
                        break;
                }

                 if (facets.Count == 0) continue;

                container.Facets = facets;
                
                containers.Add(container);
            }

            return containers;

        }

        private static IEnumerable<Facet> ResolveBuckets(BucketAggregate buckets, IDictionary<string, Map> map) {
            foreach (var bucket in buckets.Items) {
                var current = (KeyedBucket<object>)bucket;
                var currentKey = current.Key.ToString();
                var value = map.ContainsKey(currentKey) ? map[currentKey].Name : currentKey;
                var facet = new FacetText {
                    Id = currentKey,
                    Count = current.DocCount,
                    Value = new[] { value }
                };
                yield return facet;
            }
        }

        private static Facet ResolveStats(StatsAggregate stats) {
            return new FacetRangeNumber {
                Count = stats.Count,
                FromValue = stats.Min,
                ToValue = stats.Max
            };
        }
    }
}