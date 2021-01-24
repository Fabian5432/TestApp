using System;
using TestApp.Services.Interfaces;
using TestApp.ViewModels;
using TestApp.ViewModels.Base;

namespace TestApp.Services
{
    public class ViewModelLocatorService : IViewModelLocatorService
    {
        public T CreateViewModelInstance<T>() where T : class, IBaseViewModel
        {
            if (typeof(T) == typeof(RatesViewModel))
            {
                return new RatesViewModel(ServiceLocator.GetOpenExchangeRatesService, ServiceLocator.GetCurrencyLayerService) as T;
            }
            return null;
        }
    }
}
