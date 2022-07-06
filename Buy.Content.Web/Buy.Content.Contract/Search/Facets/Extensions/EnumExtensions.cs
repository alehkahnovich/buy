using System.ComponentModel;
using System.Linq;

namespace Buy.Content.Contract.Search.Facets.Extensions
{
    public static class EnumExtensions {
        public static string GetEnumDescription<T>(this T value) {
            var type = value.GetType();
            if (!type.IsEnum) return null;
            var field = type.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>();
            return attributes.FirstOrDefault()?.Description;
        }
    }
}
