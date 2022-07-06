using Buy.Content.Contract.Search.Facets.Extensions;
using Buy.Content.Search.Core.Types;

namespace Buy.Content.Business.Representation.Search
{
    public class FacetRangeNumber : Facet {
        public double? FromValue { get; set; }
        public double? ToValue { get; set; }
        public override string Type => FacetType.NumberRange.GetEnumDescription();
    }
}
