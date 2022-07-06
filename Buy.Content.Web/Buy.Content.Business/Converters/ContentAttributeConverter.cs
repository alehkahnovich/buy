using System.Collections.Generic;
using System.Linq;
using Buy.Content.Business.Representation.Content;
using Buy.Content.DataAccess.Domains;

namespace Buy.Content.Business.Converters
{
    public static class ContentAttributeConverter {
        public static IEnumerable<ContentAttribute> Convert(IEnumerable<Property> properties) {
            var attributes = properties.ToList();

            var roots = attributes.Where(attribute => !attribute.ParentId.HasValue).OrderBy(entry => entry.SortOrder);

            var siblings = attributes.Where(attribute => attribute.ParentId.HasValue)
                    .GroupBy(attribute => attribute.ParentId)
                    .ToDictionary(attribute => attribute.Key, attribute => attribute.OrderBy(entry => entry.SortOrder).AsEnumerable());

            foreach (var root in roots) yield return Convert(root, siblings);
        }

        private static ContentAttribute Convert(Property property, IDictionary<int?, IEnumerable<Property>> siblings) {
            return new ContentAttribute {
                Id = property.PropertyId,
                Type = property.Type,
                Name = property.Name,
                Behavior = property.Behavior,
                Control = property.Control,
                ParentId = property.ParentId,
                Siblings = siblings.ContainsKey(property.PropertyId) 
                ? siblings[property.PropertyId].Select(entry => Convert(entry, siblings))
                : Enumerable.Empty<ContentAttribute>()
            };
        }

        public static IEnumerable<Contract.Content.ContentAttribute> Convert(IEnumerable<ContentAttribute> attributes) {
            foreach (var attribute in attributes) {
                yield return 
                    new Contract.Content.ContentAttribute {
                        Id = attribute.Id,
                        Type = attribute.Type,
                        Behavior = attribute.Behavior,
                        Control = attribute.Control,
                        Name = attribute.Name,
                        Siblings = Convert(attribute.Siblings)
                    };
            }
        }
    }
}