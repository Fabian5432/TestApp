using System;
using System.Threading.Tasks;
using TestApp.Views;

namespace TestApp.Views
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var m = new MainView();
            await m.Display();
        }
    }
}