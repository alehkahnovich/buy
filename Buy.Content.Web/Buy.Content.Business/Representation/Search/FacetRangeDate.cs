using System;
using Buy.Content.Contract.Search.Facets.Extensions;
using Buy.Content.Search.Core.Types;

namespace Buy.Content.Business.Representation.Search
{
    public class FacetRangeDate : Facet {
        public DateTime? FromValue { get; set; }
        public DateTime? ToValue { get; set; }
        public override string Type => FacetType.Date.GetEnumDescription();
    }
}
