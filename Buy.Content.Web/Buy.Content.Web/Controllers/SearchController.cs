using System;
using System.Linq;
using System.Threading.Tasks;
using Buy.Content.Business.Converters;
using Buy.Content.Business.Providers.Search;
using Buy.Content.Contract.Search;
using Microsoft.AspNetCore.Mvc;

namespace Buy.Content.Web.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SearchController : ControllerBase {
        private readonly ISearchProvider _search;
        private readonly IBucketProvider _buckets;

        public SearchController(ISearchProvider search, IBucketProvider buckets) {
            _search = search;
            _buckets = buckets;
        }

        [HttpGet, Route("buckets/categories")]
        public async Task<IActionResult> GetCategories() {
            var buckets = await _buckets.GetAsync();
            return Ok(buckets.Select(bucket => bucket.Convert()));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SearchQuery query) {
            if (query == null || !query.IsValid())
                throw new ArgumentException("invalid query", nameof(query));

            var converted = query.Convert();
            var results = await _search.SearchAsync(converted);
            return Ok(results.Convert());
        }
    }
}