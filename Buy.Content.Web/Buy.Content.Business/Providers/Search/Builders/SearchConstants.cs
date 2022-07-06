namespace Buy.Content.Business.Providers.Search.Builders
{
    internal static class SearchConstants {
        internal static class Indices {
            public const string Modules = "modules";
        }

        internal static class Filters {
            public const string Global = "Global";
            public const string NaRange = "*";
        }

        internal static class Operators {
            public const string Or = "OR";
        }

        internal static class ModuleConstants {
            public const string Name = "name";
            public const string Categories = "categories";
            public const string Description = "description";
        }

        internal static class AggregationPrefixes {
            public const string AttributePrefix = "attr_";
        }

        internal static class AggregationConstants {
            public const string GlobalAttributes = "attributes_global";
            public const string Attributes = "attributes";
            public const string Categories = "categories";
            public const string Keyword = "keyword";
        }
    }
}
