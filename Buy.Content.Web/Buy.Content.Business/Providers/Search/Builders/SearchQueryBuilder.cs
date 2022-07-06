using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Buy.Content.Business.Representation.Search;
using Buy.Content.Contract.Search.Facets.Extensions;
using Buy.Content.DataAccess.Domains.DTO;
using Buy.Content.Search.Core.Contracts;
using Buy.Content.Search.Core.Types;
using Nest;

namespace Buy.Content.Business.Providers.Search.Builders
{
    internal sealed class SearchQueryBuilder : ISearchQueryBuilder {
        private static readonly string Number = FacetType.Number.GetEnumDescription();
        private static readonly string RangeNumber = FacetType.NumberRange.GetEnumDescription();
        private const int BoostX3 = 3;

        public SearchRequest<Module> Build(SearchQuery query) {
            var empty = new List<Agg>();
            var builder = new SearchRequest<Module>(Indices.Index(SearchConstants.Indices.Modules)) {
                Query = BuildQuery(query, empty),
                Aggregations = BuildAggregations(query, empty),
                From = query.From,
                Size = query.Size
            };

            return builder;
        }

        public SearchRequest<Module> Build(SearchQuery query, ICollection<Agg> agg) {
            var builder = new SearchRequest<Module>(Indices.Index(SearchConstants.Indices.Modules)) {
                Query = BuildQuery(query, agg),
                Aggregations = BuildAggregations(query, agg),
                From = query.From,
                Size = query.Size
            };

            return builder;
        }

        private static QueryContainer BuildQuery(SearchQuery query, IEnumerable<Agg> agg) {
            var should = new List<QueryContainer> {
                new MatchQuery {
                    Field = SearchConstants.ModuleConstants.Name,
                    Boost = BoostX3,
                    Query = query.Term
                },
                new MatchQuery {
                    Field = SearchConstants.ModuleConstants.Description,
                    Query = query.Term
                }
            };

            if (query.Facets != null && query.Facets.Any()) should.Add(BuildFacets(query, agg));

            var constrains = new List<QueryContainer> { new BoolQuery { Should = should } };

            var @bool = new BoolQuery {
                Must = constrains,
                Filter = new List<QueryContainer> { BuildCategoryFilter(query) }
            };

            return @bool;
        }

        private static QueryContainer BuildCategoryFilter(SearchQuery query) {
            if (!query.Category.HasValue) return new QueryContainer();

            return new QueryContainer(new TermQuery {
                Field = SearchConstants.ModuleConstants.Categories,
                Value = query.Category
            });
        }

        private static QueryContainer BuildFacets(SearchQuery query, IEnumerable<Agg> agg) {
            var queries = query.Facets.Join(agg,
                facet => facet.Id,
                raw => raw.RawKey, (facet, raw) => $"{BuildAggregationField(raw)}:{BuildQueryString((dynamic)facet)}");

            return new QueryStringQuery {
                Query = string.Join(' ', queries),
                DefaultOperator = Operator.And
            };
        }

        private static AggregationDictionary BuildAggregations(SearchQuery query, ICollection<Agg> agg) {
            if (!query.Category.HasValue || query.Facets == null) return new AggregationDictionary();

            var filters = agg.Join(query.Facets, facet => facet.RawKey, raw => raw.Id,
                (raw, facet) => new {
                    Facet = raw.AggKey,
                    Query = $"{BuildAggregationField(raw)}:{BuildQueryString((dynamic)facet)}"
                }).ToDictionary(entry => entry.Facet, entry => entry.Query);

            
            var aggregations = new AggregationDictionary();
            foreach (var filter in agg) {
                var container = new AggregationContainer {
                    Filter = BuildAggregationFilters(filter, filters),
                    Aggregations = new AggregationDictionary {
                        { filter.AggKey, BuildAggregationContainer(filter) }
                    }
                };

                aggregations.Add(filter.RawKey, container);
            }

            return new AggregationDictionary {
                {
                    SearchConstants.AggregationConstants.GlobalAttributes,
                    new AggregationContainer { Aggregations = aggregations, Global = new GlobalAggregation(SearchConstants.Filters.Global) } 
                }
            };
        }

        private static FilterAggregation BuildAggregationFilters(Agg agg, IDictionary<string, string> filters) {
            var query = filters.Where(entry => !string.Equals(entry.Key, agg.AggKey, StringComparison.Ordinal))
                .Select(entry => entry.Value).ToList();
            if (query.Count == 0)
                return new FilterAggregation(agg.AggKey) {
                    Filter = new MatchAllQuery()
                };

            return new FilterAggregation(agg.AggKey) {
                Filter = new QueryStringQuery { Query = string.Join(' ', query), DefaultOperator = Operator.And },
            };
        }

        private static AggregationContainer BuildAggregationContainer(Agg agg) {
            var field = BuildAggregationField(agg);
            if (string.Equals(agg.Type, RangeNumber, StringComparison.Ordinal))
                return new StatsAggregation(agg.AggKey, field);

            return new TermsAggregation(agg.AggKey) {
                Field = field,
                MinimumDocumentCount = 0,
                Order = new List<TermsOrder> { TermsOrder.KeyAscending }
            };
        }

        private static string BuildAggregationField(Agg agg) => $"{SearchConstants.AggregationConstants.Attributes}.{agg.AggKey}.{BuildAggregationFieldType(agg)}";

        private static string BuildAggregationFieldType(Agg agg) =>
            string.Equals(agg.Type, Number, StringComparison.Ordinal) || string.Equals(agg.Type, RangeNumber, StringComparison.Ordinal)
            ? Number 
            : SearchConstants.AggregationConstants.Keyword;

        private static string BuildQueryString(FacetText text) {
            var builder = new StringBuilder();
            for (var step = 0; step < text.Value.Length; step++) {
                var @operator = string.Empty;
                if (step < text.Value.Length - 1) @operator = $" {SearchConstants.Operators.Or} ";
                builder.Append($"{text.Value[step]}{@operator}");
            }

            return $"({builder})";
        }

        private static string BuildQueryString(FacetNumber text) {
            var builder = new StringBuilder();
            for (var step = 0; step < text.Value.Length; step++) {
                var @operator = string.Empty;
                if (step < text.Value.Length - 1) @operator = $" {SearchConstants.Operators.Or} ";
                builder.Append($"{text.Value[step]}{@operator}");
            }

            return $"({builder})";
        }

        private static string BuildQueryString(FacetRangeNumber range) {
            return $"[{range.FromValue?.ToString() ?? SearchConstants.Filters.NaRange} TO {range.ToValue?.ToString() ?? SearchConstants.Filters.NaRange}]";
        }
    }
}