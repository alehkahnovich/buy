using System.Collections.Generic;
using Buy.Content.Business.Providers.Search.Builders;
using Buy.Content.Search.Core.Contracts;
using Nest;

namespace Buy.Content.Business.Providers.Search
{
    internal sealed class AggregationBuilder : IAggregationBuilder {
        private const int Size = 0;
        public SearchRequest<Module> Build() {
            var builder = new SearchRequest<Module>(Indices.Index(SearchConstants.Indices.Modules)) {
                Aggregations = BuildAggregations(),
                Size = Size
            };

            return builder;
        }

        private static AggregationDictionary BuildAggregations() {
            return new Dictionary<string, IAggregationContainer> {
                {
                    SearchConstants.AggregationConstants.Categories,
                    new AggregationContainer {
                        Terms = new TermsAggregation(SearchConstants.AggregationConstants.Categories) {
                            Field = SearchConstants.AggregationConstants.Categories
                        }
                    }
                }
            };
        }
    }
}