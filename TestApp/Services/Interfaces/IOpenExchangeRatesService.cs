using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Services.Interfaces
{
    public interface IOpenExchangeRatesService
    {
        Task<List<RatesModel>> GetAllRates();
    }
}
