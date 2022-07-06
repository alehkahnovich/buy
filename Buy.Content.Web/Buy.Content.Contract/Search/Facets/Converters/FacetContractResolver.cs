using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Buy.Content.Contract.Search.Facets.Converters
{
    internal sealed class FacetContractResolver : DefaultContractResolver {
        protected override JsonConverter ResolveContractConverter(Type objectType) {
            //ignore facet converters
            if (typeof(Facet).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null;
            return base.ResolveContractConverter(objectType);
        }
    }
}
