using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rendition.Server.Configuration;
using Rendition.Server.Exceptions;
using Rendition.Server.Interfaces;

namespace Rendition.Server
{
    public class PdfRepository : IPdfRepository
    {
        private readonly ILogger<PdfRepository> _logger;
        private readonly IOptions<RenditionServerConfiguration> _options;

        public PdfRepository(ILogger<PdfRepository> logger, IOptions<RenditionServerConfiguration> options)
        {
            _logger = logger;
            _options = options;
        }

        public async Task<string> CreateAsync(byte[] payload)
        {
            var id = $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
            var path = Path.Combine(_options.Value.OutputPath, $"{id}.pdf");

            if(!Directory.Exists(_options.Value.OutputPath))
            {
                Directory.CreateDirectory(_options.Value.OutputPath);
            }
            
            await File.WriteAllBytesAsync(path, payload);

            return id;
        }

        public void Delete(string id)
        {
            var path = Path.Combine(_options.Value.OutputPath, $"{id}.pdf");
            File.Delete(path);
        }

        public async Task<byte[]> GetAsync(string id)
        {
            var path = Path.Combine(_options.Value.OutputPath, $"{id}.pdf");

            if(!File.Exists(path))
            {
                throw new NotFoundException();
            }

            var bytes = await File.ReadAllBytesAsync(path);
            
            return bytes;
        }

        public Stream GetStream(string id)
        {
             var path = Path.Combine(_options.Value.OutputPath, $"{id}.pdf");

            if(!File.Exists(path))
            {
                throw new NotFoundException();
            }

            return File.Open(path, FileMode.Open, FileAccess.Read);
        }
    }
}