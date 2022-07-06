using System.Collections.Generic;
using Nest;

namespace Buy.Content.Search.Core.Contracts.Facets
{
    public class Number : Property {
        [PropertyName("value")]
        public IEnumerable<double?> Value { get; set; }
    }
}