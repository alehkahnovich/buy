using Buy.Content.Business.Providers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Buy.Content.Business.Converters;
using Buy.Content.Contract.Module;

namespace Buy.Content.Web.Controllers
{
    [ApiController, Route("api/content/[controller]")]
    public class ModuleController : ControllerBase {
        private readonly IContentUnitProvider _provider;
        public ModuleController(IContentUnitProvider provider) {
            _provider = provider;
        }

        [HttpGet, Route("{key}", Name = "ModuleController.Read")]
        public IActionResult Get(int key) => Ok();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContentUnit content) {
            var unit = await _provider.CreateAsync(ContentUnitConverter.Convert(content));
            return CreatedAtRoute("ModuleController.Read", new { key = unit.Id }, unit);
        }
    }
}