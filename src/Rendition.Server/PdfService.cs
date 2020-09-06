using System;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RazorEngine.Templating;
using Rendition.Core.Interfaces;
using Rendition.Core.Models;
using Rendition.Server.Interfaces;

namespace Rendition.Server
{
    public class PdfService : IPdfService
    {
        private readonly ILogger<PdfService> _logger;
        private readonly IPdfRepository _repository;
        private readonly IPdfRenderer _renderer;

        public PdfService(ILogger<PdfService> logger, IPdfRenderer renderer, IPdfRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _renderer = renderer;
        }

        public async Task<RenderResponse> RenderHtmlAsync(HtmlRenderRequest request)
        {
            var response = new RenderResponse();
            byte[] bytes;

            try
            {
                bytes = await _renderer.RenderHtmlAsync(request.Html, request.Options);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add($"Error while trying to convert rendered html to a pdf {ex}");
                return response;
            }

            try
            {
                response.Id = await _repository.CreateAsync(bytes);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add($"Error while trying to persist the pdf {ex}");
                return response;
            }

            response.IsSuccessful = true;
            return response;
        }

        public async Task<RenderResponse> RenderRazorAsync(RazorRenderRequest request)
        {
            var response = new RenderResponse();
            var compiled = string.Empty;
            var obj = new ExpandoObject();
            byte[] bytes;

            try
            {
                obj = JsonConvert.DeserializeObject<ExpandoObject>(request.JsonContent, new ExpandoObjectConverter());
                compiled = RazorEngine.Engine.Razor.RunCompile(request.RazorTemplate, Guid.NewGuid().ToString(), null, obj);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add($"Error while trying to render razor content: {ex}");
                return response;
            }

            try
            {
                bytes = await _renderer.RenderHtmlAsync(compiled, request.Options);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add($"Error while trying to convert rendered html to a pdf {ex}");
                return response;
            }

            try
            {
                response.Id = await _repository.CreateAsync(bytes);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add($"Error while trying to persist the pdf {ex}");
                return response;
            }

            response.IsSuccessful = true;
            return response;
        }

        public async Task<RenderResponse> RenderUrlAsync(UrlRenderRequest request)
        {
            var response = new RenderResponse();
            byte[] bytes;

            try
            {
                bytes = await _renderer.RenderUrlAsync(request.Url, request.Options);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add($"Error while trying to render the url to a pdf {ex}");
                return response;
            }

            try
            {
                response.Id = await _repository.CreateAsync(bytes);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add($"Error while trying to persist the pdf {ex}");
                return response;
            }


            response.IsSuccessful = true;
            return response;
        }
    }
}
