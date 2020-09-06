using System.IO;

namespace Rendition.Server.Configuration
{
    public class RenditionServerConfiguration
    {
        public string InstallPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "install");
        public string TempDataPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "temp");
        public string OutputPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "data");
    }
}
