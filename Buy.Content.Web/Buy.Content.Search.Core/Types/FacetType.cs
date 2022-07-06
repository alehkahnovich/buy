using System.ComponentModel;

namespace Buy.Content.Search.Core.Types
{
    public enum FacetType {
        [Description("number")]
        Number,
        [Description("date")]
        Date,
        [Description("string")]
        Text,
        [Description("numberrange")]
        NumberRange
    }
}