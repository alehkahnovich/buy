using System.Threading.Tasks;
using Buy.Content.Business.Converters;
using Buy.Content.Business.Providers.Abstractions;
using Buy.Content.Contract.Content;
using Microsoft.AspNetCore.Mvc;

namespace Buy.Content.Web.Controllers
{
    [ApiController, Route("api/content/[controller]")]
    public class CategoryController : ControllerBase {
        private readonly ICategoryProvider _provider;
        private readonly IPropertyProvider _properties;
        public CategoryController(ICategoryProvider provider, 
            IPropertyProvider properties) {
            _provider = provider;
            _properties = properties;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var categories = await _provider.GetAsync();
            return Ok(categories);
        }

        [HttpGet, Route("{key}/properties")]
        public async Task<IActionResult> GetProperties(int key) {
            var properties = await _properties.GetAttributesAsync(key);
            return Ok(ContentAttributeConverter.Convert(properties));
        }

        [HttpGet, Route("{key}", Name = "CategoryController.Read")]
        public async Task<IActionResult> Get(int key) {
            var category = await _provider.GetAsync(key);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Category category) {
            var result = await _provider.SaveAsync(category);
            return CreatedAtRoute("CategoryController.Read", new { key = result.CategoryId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Category category) {
            await _provider.UpdateAsync(category);
            return Ok();
        }

        [HttpDelete, Route("{key}")]
        public async Task<IActionResult> Delete(int key) {
            await _provider.DeleteAsync(key);
            return Ok();
        }
    }
}