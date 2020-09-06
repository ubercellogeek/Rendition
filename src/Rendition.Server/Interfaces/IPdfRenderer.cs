using System.Threading.Tasks;
using PlaywrightSharp;
using Rendition.Core.Models;

namespace Rendition.Server.Interfaces
{
    public interface IPdfRenderer
    {
         Task<byte[]> RenderHtmlAsync(string html, RenderOptions options);
         Task<byte[]> RenderUrlAsync(string url, RenderOptions options);
    }
}