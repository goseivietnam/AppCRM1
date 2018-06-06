using AppCRM.ViewModels;
using AppCRM.ViewModels.Base;
using AppCRM.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCRM.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateToAsync(Type viewModelType);

        Task NavigateToAsync(Type viewModelType, object parameter);
    }
    public partial class NavigationService : INavigationService
    {
        #region Property
        protected readonly Dictionary<Type, Type> _mappings;
        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }
        #endregion

        #region Constructor
        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }
        #endregion

        #region Implement method
        public async Task InitializeAsync()
        {
            await NavigateToAsync<LoginViewModel>();
        }
        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }
        #endregion

        #region method extension
        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);
            CurrentApplication.MainPage = page;
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            try
            {
                ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
                page.BindingContext = viewModel;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }
        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(LoginViewModel), typeof(LoginPage));
            _mappings.Add(typeof(MainViewModel), typeof(MainPage));
        }
        #endregion
    }
}
