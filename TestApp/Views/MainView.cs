using System;
using System.Threading.Tasks;

namespace TestApp.Views
{
    public class MainView
    {
        private  RatesView RatesView;

        public MainView()
        {
            RatesView = new RatesView();
        }

        public async Task Display()
        {
            Console.WriteLine("-----------Test-----------");
            Console.WriteLine("Available commands: \n first_api (open exchange rates api) \n second_api (currency layer api)" +
                             "\n search_rate (display current rates) \n best_rate (display best current rate via 2 api's) \n");

            while (true)
            {
                Console.WriteLine("Insert command:");
                await ParseCommand(Console.ReadLine());
                Console.ReadLine();
            }
        }

        private async Task ParseCommand(string command)
        {
            switch (command)
            {
                case ("first_api"):
                    Console.WriteLine("All rates from USD Open Exchange api:" + "\n");
                    await RatesView.ShowRatesOpenExchangeRatesApi();
                    break;
                case ("second_api"):
                    Console.WriteLine("All rates from USD Currency layer api:" + "\n");
                    await RatesView.ShowQuatesCurrencyLayerApi();
                    break;
                case ("search_rate"):
                    Console.WriteLine("Search current rate from USD to: ");
                    await RatesView.ShowCurrentRateValue(Console.ReadLine());
                    break;
                case ("best_rate"):
                    Console.WriteLine("Search best rate from USD to: ");
                    await RatesView.ShowBestCurrentRateValue(Console.ReadLine());
                    break;
                default:
                    break;
            }
        }
    }
}
