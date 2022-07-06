using System.Collections.Generic;
using Nest;

namespace Buy.Content.Search.Core.Contracts.Facets
{
    public class Text : Property {
        [PropertyName("value")]
        public IEnumerable<string> Value { get; set; }
    }
}