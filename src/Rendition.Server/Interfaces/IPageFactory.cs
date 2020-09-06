using System.Threading.Tasks;
using PlaywrightSharp;

namespace Rendition.Server.Interfaces
{
    public interface IPageFactory
    {
         Task<IPage> CreatePageAsync();
    }
}