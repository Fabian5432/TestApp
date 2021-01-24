using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Services.Interfaces
{
    public interface ICurrencyLayerService
    {
        Task<List<QuotesModel>> GetAllQuotes();
    }
}
