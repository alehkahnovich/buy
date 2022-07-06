using System.Collections.Generic;
using Buy.Content.Business.Representation.Search;
using Buy.Content.DataAccess.Domains.DTO;
using Buy.Content.Search.Core.Contracts;
using Nest;

namespace Buy.Content.Business.Providers.Search.Builders
{
    public interface ISearchQueryBuilder {
        SearchRequest<Module> Build(SearchQuery query);
        SearchRequest<Module> Build(SearchQuery query, ICollection<Agg> agg);
    }
}