using System.Threading.Tasks;

namespace Mood.Services
{
    public interface IHttpService
    {
        Task<string> Get(string url);
    }
}