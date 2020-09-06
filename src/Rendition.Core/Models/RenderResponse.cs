using System.Collections.Generic;

namespace Rendition.Core.Models
{
    public class RenderResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string Id { get; set; }
    }
}
