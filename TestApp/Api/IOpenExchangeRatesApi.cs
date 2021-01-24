using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestApp.Api
{
    public interface IOpenExchangeRatesApi
    {
        [Get("/api/latest.json")]
        Task <HttpResponseMessage> GetLatest([Query] string app_id);
    }
}
