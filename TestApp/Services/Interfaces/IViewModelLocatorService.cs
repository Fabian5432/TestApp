using System;
using TestApp.ViewModels.Base;

namespace TestApp.Services.Interfaces
{
    public interface IViewModelLocatorService
    {
        T CreateViewModelInstance<T>()
            where T : class, IBaseViewModel;
    }
}
