using System.Net.Mime;
using System.Threading.Tasks;
using Buy.Upload.Business.Services.Abstractions;
using Buy.Upload.Web.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Buy.Upload.Web.Controllers
{
    [ApiController, Route("api/content/[controller]")]
    public class ArtifactController : ControllerBase {
        private readonly IArtifactService _artifactService;

        public ArtifactController(IArtifactService artifactService) {
            _artifactService = artifactService;
        }

        [HttpGet, Route("{key}")]
        public async Task<IActionResult> GetArtifact(int key) {
            var artifact = await _artifactService.GetArtifactAsync(key);
            if (artifact == null)
                return NoContent();

            var disposition = new ContentDisposition {
                FileName = $"{artifact.Id}-{key}",
                Inline = true
            };

            Response.Headers.Add("Content-Disposition", disposition.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(artifact.Stream, artifact.ContentType);
        }
    }
}