using System.Threading.Tasks;
using Rendition.Core.Models;

namespace Rendition.Core.Interfaces
{
    public interface IPdfService
    {
        Task<RenderResponse> RenderUrlAsync(UrlRenderRequest request);
        Task<RenderResponse> RenderHtmlAsync(HtmlRenderRequest request);
        Task<RenderResponse> RenderRazorAsync(RazorRenderRequest request);
    }
}