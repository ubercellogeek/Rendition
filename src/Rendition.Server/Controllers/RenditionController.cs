using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Rendition.Core.Interfaces;
using Rendition.Core.Models;
using Rendition.Server.Exceptions;
using Rendition.Server.Interfaces;

namespace Rendition.Server.Controllers
{
    public class RenditionController : ControllerBase
    {
        private readonly ILogger<RenditionController> _logger;
        private readonly IPdfService _service;
        private readonly IPdfRepository _repository;

        public RenditionController(ILogger<RenditionController> logger, IPdfService service, IPdfRepository repository)
        {
            _logger = logger;
            _service = service;
            _repository = repository;
        }

        [HttpPost("/render/url")]
        public async Task<IActionResult> RenderUrl([FromBody]UrlRenderRequest request)
        {
            var result = await _service.RenderUrlAsync(request);
            return Ok(result);
        }

        [HttpPost("/render/html")]
        public async Task<IActionResult> RenderHtml([FromBody]HtmlRenderRequest request)
        {
            var result = await _service.RenderHtmlAsync(request);
            return Ok(result);
        }

        [HttpPost("/render/razor")]
        public async Task<IActionResult> RenderRazor([FromBody]RazorRenderRequest request)
        {
            var result = await _service.RenderRazorAsync(request);
            if(!result.IsSuccessful)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("/download/{id}")]
        public async Task<IActionResult> Download(string id)
        {
            try
            {
                var bytes = await _repository.GetAsync(id);
                var result = new FileContentResult(bytes, new MediaTypeHeaderValue("application/pdf")) {
                    FileDownloadName = $"{id}.pdf"
                };

                return result;
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("/view/{id}")]
        public async Task<IActionResult> View(string id)
        {
            try
            {
                var bytes = await _repository.GetAsync(id);
                return File(bytes, "application/pdf");
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}