using Buy.Content.Contract.Search.Facets.Extensions;
using Buy.Content.Search.Core.Types;

namespace Buy.Content.Business.Representation.Search
{
    public class FacetText : Facet {
        public string[] Value { get; set; }
        public override string Type => FacetType.Text.GetEnumDescription();
    }
}
