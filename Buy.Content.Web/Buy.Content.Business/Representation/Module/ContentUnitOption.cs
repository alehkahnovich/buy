using System.Collections.Generic;

namespace Buy.Content.Business.Representation.Module
{
    public class ContentUnitOption {
        public int Id { get; set; }
        public IEnumerable<ContentUnitOptionValue> Values { get; set; }
    }
}