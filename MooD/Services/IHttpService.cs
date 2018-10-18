using System.Threading.Tasks;

namespace MooD.Services
{
    public interface IHttpService
    {
        Task<string> Get(string url);
    }
}