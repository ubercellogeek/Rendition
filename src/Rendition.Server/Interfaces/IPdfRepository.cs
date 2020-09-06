using System.IO;
using System.Threading.Tasks;

namespace Rendition.Server.Interfaces
{
    public interface IPdfRepository
    {
        Task<string> CreateAsync(byte[] payload);
        Task<byte[]> GetAsync(string id);
        Stream GetStream(string id);
        void Delete(string id);

    }
}
