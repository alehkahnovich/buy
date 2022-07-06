using Buy.Content.Contract.Search.Facets.Extensions;
using Buy.Content.Search.Core.Types;

namespace Buy.Content.Business.Representation.Search
{
    public class FacetNumber : Facet {
        public double?[] Value { get; set; }
        public override string Type => FacetType.Number.GetEnumDescription();
    }
}
