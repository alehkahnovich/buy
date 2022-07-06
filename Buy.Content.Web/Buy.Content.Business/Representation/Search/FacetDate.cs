using System;
using Buy.Content.Contract.Search.Facets.Extensions;
using Buy.Content.Search.Core.Types;

namespace Buy.Content.Business.Representation.Search
{
    public class FacetDate : Facet {
        public DateTime?[] Value { get; set; }
        public override string Type => FacetType.Date.GetEnumDescription();
    }
}
