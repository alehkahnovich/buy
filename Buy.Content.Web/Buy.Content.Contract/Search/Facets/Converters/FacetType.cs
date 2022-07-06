using System.ComponentModel;

namespace Buy.Content.Contract.Search.Facets.Converters
{
    internal enum FacetType {
        [Description("number")]
        Number,
        [Description("numberrange")]
        NumberRange,
        [Description("money")]
        Money,
        [Description("date")]
        Date,
        [Description("string")]
        Text
    }
}
