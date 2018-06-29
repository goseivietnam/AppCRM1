using AppCRM.ViewModels;
using AppCRM.ViewModels.Account;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate;
using AppCRM.ViewModels.Main.Candidate.Job;
using AppCRM.ViewModels.Main.Candidate.Profile;
using AppCRM.Views;
using AppCRM.Views.Account;
using AppCRM.Views.Main.Candidate;
using AppCRM.Views.Main.Candidate.ExplorePage;
using AppCRM.Views.Main.Candidate.JobPage;
using AppCRM.Views.Main.Candidate.ProfilePage;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
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

        Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase;

        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase;
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

        public Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase
        {
            return NavigateToPopupAsync<TViewModel>(null, animate);
        }

        public async Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase
        {
            var page = CreateAndBindPage(typeof(TViewModel), parameter);
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            if (page is PopupPage)
            {
                await PopupNavigation.Instance.PushAsync(page as PopupPage, animate);
            }
            else
            {
                throw new ArgumentException($"The type ${typeof(TViewModel)} its not a PopupPage type");
            }
        }
        #endregion

        #region method extension
        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);
            if (page is CandidateMainPage || page is LoginPage)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is CandidateMainPage)
            {
                var candidateMainPage = CurrentApplication.MainPage as CandidateMainPage;
                var navigationPage = candidateMainPage.Detail as NavigationPage;
                if (navigationPage != null)
                {
                    var currentPage = navigationPage.CurrentPage;
                    if (currentPage.GetType() != page.GetType())
                    {
                        navigationPage = new NavigationPage(page);
                        candidateMainPage.Detail = navigationPage;
                    }
                }
                else
                {
                    navigationPage = new NavigationPage(page);
                    candidateMainPage.Detail = navigationPage;
                }
                candidateMainPage.IsPresented = false;
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as NavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new NavigationPage(page);
                }
            }
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }
            try
            {
                Page page = Activator.CreateInstance(pageType) as Page;
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
            _mappings.Add(typeof(CandidateProfileViewModel), typeof(CandidateProfilePage));
            _mappings.Add(typeof(CandidateJobViewModel), typeof(CandidateJobPage));
            _mappings.Add(typeof(CandidateMainViewModel), typeof(CandidateMainPage));
            _mappings.Add(typeof(CandidateRegisterViewModel), typeof(CandidateRegisterPage));
            _mappings.Add(typeof(EmployerRegisterViewModel), typeof(EmployerRegisterPage));
            _mappings.Add(typeof(RegisterPopupViewModel), typeof(RegisterPopupPage));
            _mappings.Add(typeof(AddEducationViewModel), typeof(AddEducationPage));
            _mappings.Add(typeof(AddDocumentViewModel), typeof(AddDocumentPage));
            _mappings.Add(typeof(AddLicenceViewModel), typeof(AddLicencePage));
            _mappings.Add(typeof(AddQualificationViewModel), typeof(AddQualificationPage));
            _mappings.Add(typeof(AddReferenceViewModel), typeof(AddReferencePage));
            _mappings.Add(typeof(AddSkillPageViewModel), typeof(AddSkillPage));
            _mappings.Add(typeof(AddWorkExprienceViewModel), typeof(AddWorkExpriencePage));
            _mappings.Add(typeof(EditProfileViewModel), typeof(EditProfilePage));
            _mappings.Add(typeof(AccountSettingViewModel), typeof(AccountSettingPage));
            _mappings.Add(typeof(JobDetailViewModel), typeof(JobDetailPage));
            _mappings.Add(typeof(CandidateExploreViewModel), typeof(CandidateExplorePage));
        }
        #endregion
    }
}
