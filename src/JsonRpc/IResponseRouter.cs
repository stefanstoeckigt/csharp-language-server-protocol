using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OmniSharp.Extensions.JsonRpc
{
    public interface IResponseRouter
    {
        void SendNotification(string method);
        void SendNotification<T>(string method, T @params);
        Task<TResponse> SendRequest<T, TResponse>(string method, T @params);
        Task<TResponse> SendRequest<TResponse>(string method);
        Task SendRequest<T>(string method, T @params);
        TaskCompletionSource<JToken> GetRequest(long id);
    }
}
