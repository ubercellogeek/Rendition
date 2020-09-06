using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Rendition.Core.Interfaces;
using Rendition.Core.Models;

namespace Rendition.Client
{
    public class PdfService : IPdfService
    {
        private readonly HttpClient _client;
        public PdfService(HttpClient client)
        {
            _client = client;
        }

        public async Task<RenderResponse> RenderHtmlAsync(HtmlRenderRequest request)
        {
            var response = await Render(request, "html");
            return response;
        }

        public async Task<RenderResponse> RenderRazorAsync(RazorRenderRequest request)
        {
            var response = await Render(request, "razor");
            return response;
        }

        public async Task<RenderResponse> RenderUrlAsync(UrlRenderRequest request)
        {
            var response = await Render(request, "url");
            return response;
        }

        private async Task<RenderResponse> Render<T>(T request, string pathFragment)
        {
            var response = new RenderResponse();

            try
            {
                var httpResult = await _client.PostAsJsonAsync($"render/{pathFragment}", request);

                if (httpResult.IsSuccessStatusCode)
                {
                    response = await httpResult.Content.ReadFromJsonAsync<RenderResponse>();
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Errors.Add($"There was an error response received from the API. Code: {httpResult.StatusCode}");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add($"There was an error while communicating with the API: {ex.Message}");
                return response;
            }
        }
    }
}
