using System;
using System.Collections.Generic;
using Nest;

namespace Buy.Content.Search.Core.Contracts.Facets
{
    public class Date : Property {
        [PropertyName("value")]
        public IEnumerable<DateTime?> Value { get; set; }
    }
}