using System.Collections.Generic;
using System.Linq;
using Buy.Content.DataAccess.Domains.DTO;

namespace Buy.Content.Business.Representation.Buckets
{
    internal sealed class AggHierarchy {
        private readonly IEnumerable<Agg> _agg;
        public AggHierarchy(IEnumerable<Agg> agg) => _agg = agg;

        public IDictionary<string, Agg> GetRoots() => _agg.Where(entry => string.IsNullOrEmpty(entry.RootAggKey))
            .ToDictionary(entry => entry.RawKey, entry => entry);

        public IDictionary<string, IEnumerable<Agg>> GeLeafs() => _agg.Where(entry => !string.IsNullOrEmpty(entry.RootAggKey))
            .GroupBy(entry => entry.RootAggKey).ToDictionary(entry => entry.Key, entry => entry.AsEnumerable());
    }
}