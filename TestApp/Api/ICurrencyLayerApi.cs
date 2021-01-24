using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace TestApp.Api
{
    public interface ICurrencyLayerApi
    {
        [Get("/api/live")]
        Task<HttpResponseMessage> GetLiveData([Query] string access_key);
    }
}
