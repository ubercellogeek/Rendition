using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlaywrightSharp;
using Rendition.Core.Models;
using Rendition.Server.Extensions;
using Rendition.Server.Interfaces;

namespace Rendition.Server
{
    public class PdfRenderer : IPdfRenderer
    {
        private readonly ILogger<PdfRenderer> _logger;
        private readonly IPageFactory _factory;

        public PdfRenderer(ILogger<PdfRenderer> logger, IPageFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        public async Task<byte[]> RenderHtmlAsync(string html, RenderOptions options)
        {
            IPage page = default;
            try
            {
                page = await _factory.CreatePageAsync();

                await page.SetContentAsync(html);
                var result = await page.GetPdfDataAsync(options.AsPdfOptions());

                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                if (page != null) await page.CloseAsync();
            }

        }

        public async Task<byte[]> RenderUrlAsync(string url, RenderOptions options)
        {
            IPage page = default;
            try
            {
                page = await _factory.CreatePageAsync();

                var response = await page.GoToAsync(url);
                await page.EmulateMediaAsync(new EmulateMedia() { Media = MediaType.Screen });
                var result = await page.GetPdfDataAsync(options.AsPdfOptions());

                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                if (page != null) await page.CloseAsync();
            }
        }

        public Task<byte[]> RenderAsync(RazorRenderRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}