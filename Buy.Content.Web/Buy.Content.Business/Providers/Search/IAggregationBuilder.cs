using Buy.Content.Search.Core.Contracts;
using Nest;

namespace Buy.Content.Business.Providers.Search
{
    public interface IAggregationBuilder {
        SearchRequest<Module> Build();
    }
}