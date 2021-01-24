using System;
using TestApp.Services;
using TestApp.Services.Interfaces;
using TestApp.ViewModels.Base;

namespace TestApp.Views.Base
{
    public abstract class BaseView<TViewModel> where TViewModel : class, IBaseViewModel
    {
        #region Fields

        private readonly IViewModelLocatorService _viewModelLocatorService;

        #endregion

        #region Properties

        public TViewModel ViewModel { get; set; }

        #endregion

        #region Constructor

        protected BaseView()
        {
            _viewModelLocatorService = new ViewModelLocatorService();
            ViewModel = _viewModelLocatorService.CreateViewModelInstance<TViewModel>();
        }

        public BaseView(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        #endregion
    }
}
