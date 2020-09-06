using PlaywrightSharp;

namespace Rendition.Core.Models
{
    public class UrlRenderRequest
    {
        public string Url { get; set; }
        public RenderOptions Options { get; set; }
    }
}