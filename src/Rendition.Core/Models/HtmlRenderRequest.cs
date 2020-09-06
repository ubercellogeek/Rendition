using PlaywrightSharp;

namespace Rendition.Core.Models
{
    public class HtmlRenderRequest
    {
        public string Html { get; set; }
        public RenderOptions Options { get; set; }
    }
}