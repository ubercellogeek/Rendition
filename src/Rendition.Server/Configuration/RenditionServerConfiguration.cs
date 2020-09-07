using System.IO;

namespace Rendition.Server.Configuration
{
    public class RenditionServerConfiguration
    {
        public string InstallPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), ".local-data\\install");
        public string TempDataPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), ".local-data\\temp");
        public string OutputPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), ".local-data\\output");
    }
}
