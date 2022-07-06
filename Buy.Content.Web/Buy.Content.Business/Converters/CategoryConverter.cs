using System.Linq;
using Buy.Content.Business.Representation.Search;

namespace Buy.Content.Business.Converters
{
    public static class CategoryConverter {
        public static Contract.Search.Category Convert(this Category category) => 
            new Contract.Search.Category {
                Id = category.Id,
                Count = category.Count,
                Name = category.Name,
                Categories = category.Siblings?.Select(Convert)
            };
    }
}