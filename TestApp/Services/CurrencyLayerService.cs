using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestApp.Api;
using TestApp.Dto;
using TestApp.Models;
using TestApp.Services.Interfaces;

namespace TestApp.Services
{
    public class CurrencyLayerService : ICurrencyLayerService
    {
        #region Fields

        private readonly ICurrencyLayerApi _currencyLayerApi;

        #endregion

        #region Properties

        public string Access_Key { get; set; }

        #endregion

        #region Constructor

        public CurrencyLayerService(string access_key)
        {
            _currencyLayerApi = RestService.For<ICurrencyLayerApi>("http://api.currencylayer.com");
            Access_Key = access_key ?? throw new ArgumentNullException(nameof(access_key));
        }

        #endregion

        #region Methods

        public async Task<List<QuotesModel>> GetAllQuotes()
        {
            var allQuotes = new List<QuotesModel>();
            string body;
            HttpResponseMessage response;

            try
            {
                response = await _currencyLayerApi.GetLiveData(access_key: Access_Key);
                body = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<CurrencyLayerDto>(body);
                foreach (var q in data.Quotes)
                {
                    allQuotes.Add(new QuotesModel(quoteName: q.Key, quoteValue: q.Value));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return allQuotes;
        }

        #endregion
    }
}
