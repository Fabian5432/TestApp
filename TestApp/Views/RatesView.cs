using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestApp.Views.Base;
using TestApp.ViewModels;

namespace TestApp.Views
{
    public class RatesView : BaseView<RatesViewModel>
    {   
        public async Task ShowRatesOpenExchangeRatesApi()
        {
            await ViewModel.GetAllRates();

            foreach (var openExchangeRate in ViewModel.RatesItems)
            {
                Console.WriteLine(openExchangeRate.RateName + ":" + openExchangeRate.RateValue);
            }
        }

        public async Task ShowQuatesCurrencyLayerApi()
        {
            await ViewModel.GetAllQuotes();
            foreach (var currencyLayerQuate in ViewModel.QuotesItems)
            {
                Console.WriteLine(currencyLayerQuate.QuoteName + ":" + currencyLayerQuate.QuoteValue);
            }
        }

        public async Task ShowCurrentRateValue(string currency)
        {
            var openExchangeApiRate = await ViewModel.GetCurrentRateFor(currency);
            var currencyLayerApiQuote = await ViewModel.GetCurrentQuoteFor(currency);

            Print($"Current rate from USD to {currency} via Open Exchange api: " + openExchangeApiRate.RateValue);
            Print($"Current rate from USD to {currency} via Currency layer api: " + currencyLayerApiQuote.QuoteValue);
        }

        public async Task ShowBestCurrentRateValue(string currency)
        {
            var openExchangeApiRate = await ViewModel.GetCurrentRateFor(currency);
            var currencyLayerApiQuote = await ViewModel.GetCurrentQuoteFor(currency);

            var bestRateValue = (openExchangeApiRate.RateValue > currencyLayerApiQuote.QuoteValue) ? openExchangeApiRate.RateValue : currencyLayerApiQuote.QuoteValue;

            Print($"Best current rate from USD to {currency}: " + bestRateValue);
        }

        #region Helper Method

        private void Print(string printData)
        {
            string[] mykey = ConfigurationManager.AppSettings["PrintMode"].Split(',');
            Console.WriteLine("Print mode: Console / MessageBox");
            var readKey = Console.ReadLine();
            if (mykey[0] == readKey)
            {
                Console.WriteLine(printData);
            }
            if (mykey[1] == readKey)
            {
                MessageBox.Show(printData);
            }
            else
            {
                Console.WriteLine("Unknown printing option");
            }
        }

        #endregion
    }
}
