using System.Threading.Tasks;

namespace Habitica.NET
{
    public interface IHabiticaClient
    {
        Task<TRes> GetAsync<TRes>(string requestUri);
        Task<TRes> PostAsync<TReq, TRes>(string requestUri, TReq requestData);
        Task<TRes> PutAsync<TReq, TRes>(string requestUri, TReq requestData);
        Task DeleteAsync(string requestUri);
    }
}
