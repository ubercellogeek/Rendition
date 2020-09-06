using PlaywrightSharp;

namespace Rendition.Core.Models
{
    public class RazorRenderRequest
    {
        public string RazorTemplate { get; set; }
        public string JsonContent { get; set; }
        public RenderOptions Options { get; set; }
    }
}