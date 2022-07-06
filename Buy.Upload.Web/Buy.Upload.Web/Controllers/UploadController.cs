using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Buy.Upload.Business.Contracts;
using Buy.Upload.Business.Services.Abstractions;
using Buy.Upload.Contracts.Uploads;
using Buy.Upload.Web.Constants;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Buy.Upload.Web.Controllers
{
    [ApiController, Route("api/content/[controller]")]
    public class UploadController : ControllerBase {
        private readonly IUploadService _uploads;
        private readonly IArtifactService _artifactService;
        public UploadController(IUploadService uploads, IArtifactService artifactService) {
            _uploads = uploads;
            _artifactService = artifactService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload() {
            var boundary = Request.GetMultipartBoundary();
            var reader = new MultipartReader(boundary, Request.Body);
            var uploads = new List<UploadedRequest>();
            var section = await reader.ReadNextSectionAsync();
            while (section != null) {
                if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var disposition))
                    return BadRequest();

                var request = await _uploads.UploadAsync(new UploadRequest(section.Body, section.ContentType) {
                    FileName = disposition.FileName.Value
                });

                uploads.Add(request);

                section = await reader.ReadNextSectionAsync();
            }

            return Ok(uploads);
        }

        [HttpGet, Route("{key}", Name = "UploadController.Read")]
        public async Task<IActionResult> Download(int key) {
            var content = await _uploads.GetAsync(key);
            if (content == null) return NotFound();
            return File(content.Stream, content.ContentType);
        }

        [HttpGet, Route("{type}/{key}", Name = "UploadController.ArtifactRead")]
        public async Task<IActionResult> GetArtifact(string type, int key) {
            var artifact = await _artifactService.GetStreamAsync(type, key);
            if (artifact == null)
                return NoContent();

            var disposition = new ContentDisposition {
                FileName = $"{type}-{key}",
                Inline = true
            };

            Response.Headers.Add("Content-Disposition", disposition.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            Response.Headers.Add(Headers.Artifact, $"{artifact.Id}");
            return File(artifact.Stream, artifact.ContentType);
        }
    }
}