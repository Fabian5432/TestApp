using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestApp.Api;
using TestApp.Services.Interfaces;
using TestApp.Models;
using TestApp.Dto;

namespace TestApp.Services
{
    public class OpenExchangeRatesService : IOpenExchangeRatesService
    {
        #region Properties

        public string App_id { get; set; }

        #endregion

        #region Fields

        private readonly IOpenExchangeRatesApi _openExchangeRatesApi;

        #endregion

        #region Constructor

        public OpenExchangeRatesService(string app_id)
        {
            _openExchangeRatesApi = RestService.For<IOpenExchangeRatesApi>("https://openexchangerates.org");
            App_id = app_id ?? throw new ArgumentNullException(nameof(app_id));
        }

        #endregion

        #region Methods

        public async Task<List<RatesModel>> GetAllRates()
        {
            var allCurrencies = new List<RatesModel>();
            string body;
            HttpResponseMessage response;

            try
            {
                response = await _openExchangeRatesApi.GetLatest(app_id: App_id);
                body = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<OpenExchangeRatesDto>(body);
                foreach (var d in data.Rates)
                {
                    allCurrencies.Add(new RatesModel(rateName: d.Key, rateValue: d.Value));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return allCurrencies;
        }

        #endregion
    }
}
