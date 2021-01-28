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
            try
            {
                await ViewModel.GetCurrentRateFor(currency);
                Print($"Current rate from USD to {currency} via Open Exchange api: " + ViewModel.OpenExchangeRatesApiRateValue + "\n"
                        + $"Current rate from USD to {currency} via Currency layer api: " + ViewModel.CurrencyLayerServiceApiValue);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task ShowBestCurrentRateValue(string currency)
        {
            try
            {
                await ViewModel.GetBestCurrentRateFor(currency);
                Print($"Best current rate from USD to {currency}: " + ViewModel.BestCurrencyValue);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
        }

        #endregion
    }
}
