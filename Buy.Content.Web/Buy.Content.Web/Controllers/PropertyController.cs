using System.Threading.Tasks;
using Buy.Content.Business.Providers.Abstractions;
using Buy.Content.Contract.Content;
using Microsoft.AspNetCore.Mvc;

namespace Buy.Content.Web.Controllers
{
    [ApiController, Route("api/content/[controller]")]
    public class PropertyController : ControllerBase {
        private readonly IPropertyProvider _provider;
        public PropertyController(IPropertyProvider provider) {
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var properties = await _provider.GetAsync();
            return Ok(properties);
        }

        [HttpGet, Route("{key}", Name = "PropertyController.Read")]
        public async Task<IActionResult> Get(int key) {
            var property = await _provider.GetAsync(key);
            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Property property) {
            var result = await _provider.SaveAsync(property);
            return CreatedAtRoute("PropertyController.Read", new { key = result.PropertyId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Property category) {
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