using System.Collections.Generic;
using System.Linq;
using Buy.Content.Business.Representation.Module;
using Buy.Content.Contract.Module;
using Buy.Content.DataAccess.Domains;
using ContentUnit = Buy.Content.Business.Representation.Module.ContentUnit;

namespace Buy.Content.Business.Converters
{
    public static class ContentUnitConverter {
        public static ContentUnit Convert(Contract.Module.ContentUnit unit) {
            return new ContentUnit {
                Id = unit.Id,
                Name = unit.Name,
                CategoryId = unit.CategoryId,
                Description = unit.Description,
                Attributes = Convert(unit.Attributes),
                Artifacts = unit.Artifacts
            };
        }

        private static IEnumerable<ContentUnitOption> Convert(IEnumerable<ContentOption> options) {
            foreach (var option in options) {
                yield return new ContentUnitOption {
                    Id = option.Id,
                    Values = Convert(option.Values)
                };
            }
        }

        private static IEnumerable<ContentUnitOptionValue> Convert(IEnumerable<ContentOptionValue> values) {
            foreach (var value in values) {
                yield return new ContentUnitOptionValue {
                    Id = value.Id,
                    Value = value.Value
                };
            }
        }

        public static ContentUnit Convert(Module unit) {
            return new ContentUnit {
                Id = unit.ModuleId,
                CategoryId = unit.CategoryId,
                Description = unit.Description,
                Name = unit.Name
            };
        }

        public static Module Convert(ContentUnit unit) {
            return new Module {
                ModuleId = unit.Id,
                CategoryId = unit.CategoryId,
                Description = unit.Description,
                Name = unit.Name,
                Properties = Convert(unit.Attributes),
                Artifacts = unit.Artifacts
            };
        }

        private static IEnumerable<ModuleProperty> Convert(IEnumerable<ContentUnitOption> attributes) {
            foreach (var attribute in attributes.SelectMany(unit => unit.Values)) {
                yield return new ModuleProperty {
                    PropertyId = attribute.Id,
                    Value = attribute.Value
                };
            }
        }
    }
}