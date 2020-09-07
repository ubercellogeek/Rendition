using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlaywrightSharp;
using PlaywrightSharp.Chromium;
using Rendition.Server.Configuration;
using Rendition.Server.Interfaces;

namespace Rendition.Server
{
    public class PageFactory : IPageFactory, IDisposable
    {
        private readonly ILogger<PageFactory> _logger;
        private IBrowser _instance;
        private readonly IBrowserType _browserType;
        private readonly IBrowserFetcher _browserFetcher;
        private readonly RenditionServerConfiguration _configuration;

        public PageFactory(ILogger<PageFactory> logger, IOptions<RenditionServerConfiguration> configuration)
        {
            _logger = logger;
            _browserType = new ChromiumBrowserType();
            _configuration = configuration.Value;
            _browserFetcher = _browserType.CreateBrowserFetcher(new BrowserFetcherOptions() {
                Path = _configuration.InstallPath
            });
        }
        
        public async Task<IPage> CreatePageAsync()
        {
            if(_instance == default || !_instance.IsConnected)
            {
                if(!Directory.Exists(_configuration.InstallPath))
                {
                    Directory.CreateDirectory(_configuration.InstallPath);
                }

                if(!Directory.Exists(_configuration.TempDataPath))
                {
                    Directory.CreateDirectory(_configuration.TempDataPath);
                }

                _logger.LogInformation("Downloading browser");
                var rinfo = await _browserFetcher.DownloadAsync();
                _logger.LogInformation("Browser downloaded to: {0}", _configuration.InstallPath);
                try
                {
                    _instance = await _browserType.LaunchAsync(new LaunchOptions() {
                    UserDataDir = _configuration.TempDataPath,
                    ExecutablePath = rinfo.ExecutablePath,
                    Headless = true
                    });
                }
                catch (System.Exception ex)
                {
                    throw;
                }
                
                _logger.LogInformation("Browser launched. Data path: {0}", _configuration.TempDataPath);
            }

            var page = await _instance.DefaultContext.NewPageAsync();
            
            return page;
        }

        public void Dispose()
        {
            if(_instance != null)
            {
                _logger.LogDebug("Disposing of browser instance");
                _instance.CloseAsync().Wait();
                _instance.Dispose();
            }
        }
    }
}
