using System;
using System.Collections.Generic;
using Buy.Content.Search.Core.Contracts.Abstractions;
using Buy.Content.Search.Core.Contracts.Facets;
using Nest;

namespace Buy.Content.Search.Core.Contracts
{
    public sealed class Module : ISearchUnit {
        [Number, PropertyName("module_id")]
        public int Id { get; set; }
        [Text(Name = "name")]
        public string Name { get; set; }
        [Text(Name = "description")]
        public string Description { get; set; }
        [Date(Format = "yyyy-MM-dd"), PropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
        [PropertyName("categories")]
        public IEnumerable<string> Categories { get; set; }
        [PropertyName("attributes")]
        public Facet Properties { get; set; }
        [PropertyName("artifacts")]
        public IEnumerable<int> Artifacts { get; set; }
    }
}