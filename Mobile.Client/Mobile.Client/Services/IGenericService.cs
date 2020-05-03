using System.Threading.Tasks;

namespace Mobile.Client.Services
{
    public interface IGenericService
    {
        Task<T> GetAsync<T>(string uri, string authToken = ""); 
        Task<TR> PostAsync<T, TR>(string uri, T data, string authToken = "") where TR: new();
        Task<TR> PutAsync<T, TR>(string uri, T data, string authToken = "") where TR : new(); 
        Task DeleteAsync(string uri, string authToken = "");
    }
}