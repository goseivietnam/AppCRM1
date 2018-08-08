using AppCRM.Services.Authentication;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate.Profile;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.TabView;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateMainViewModel : ViewModelBase
    {
        public static CandidateMainViewModel Current
        {
            get
            {
                return Application.Current.MainPage.BindingContext as CandidateMainViewModel;
            }
        }

        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public bool IsProfilePageRendered = false;
        public bool IsJobPageRendered = false;
        public bool IsExplorePageRendered = false;
        public bool IsNotifyPageRendered = false;
        public bool IsMessagePageRendered = false;

        private int _selectedIndex;
        private ViewModelBase _profilePage;
        private ViewModelBase _jobPage;
        private ViewModelBase _explorePage;
        private ViewModelBase _notifyPage;
        private ViewModelBase _messagePage;
        private string _avatarUrl;
        private string _userName;
        private TabDisplayMode _tabHeaderMode = TabDisplayMode.ImageWithText;

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }
        public ViewModelBase ProfilePage
        {
            get
            {
                return _profilePage;
            }
            set
            {
                _profilePage = value;
                OnPropertyChanged();
            }
        }
        public ViewModelBase JobPage
        {
            get
            {
                return _jobPage;
            }
            set
            {
                _jobPage = value;
                OnPropertyChanged();
            }
        }
        public ViewModelBase ExplorePage
        {
            get
            {
                return _explorePage;
            }
            set
            {
                _explorePage = value;
                OnPropertyChanged();
            }
        }
        public ViewModelBase NotifyPage
        {
            get
            {
                return _notifyPage;
            }
            set
            {
                _notifyPage = value;
                OnPropertyChanged();
            }
        }
        public ViewModelBase MessagePage
        {
            get
            {
                return _messagePage;
            }
            set
            {
                _messagePage = value;
                OnPropertyChanged();
            }
        }
        public string AvatarUrl
        {
            get
            {
                return _avatarUrl;
            }
            set
            {
                _avatarUrl = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        public TabDisplayMode TabHeaderMode
        {
            get
            {
                return _tabHeaderMode;
            }
            set
            {
                _tabHeaderMode = value;
                OnPropertyChanged();
            }
        }

        public ICommand MasterMenuItemTappedCommand => new AsyncCommand(MasterMenuItemTappedAsync);
        public ICommand SelectionChangedCommand => new Command(RenderTabContent);

        public CandidateMainViewModel()
        {
            _authenticationService = Locator.Instance.Resolve<IAuthenticationService>();
            _dialogService = Locator.Instance.Resolve<IDialogService>();
            _navigationService = Locator.Instance.Resolve<INavigationService>();

            ProfilePage = Locator.Instance.Resolve<CandidateProfileViewModel>() as ViewModelBase;
            JobPage = Locator.Instance.Resolve<CandidateJobViewModel>() as ViewModelBase;
            ExplorePage = Locator.Instance.Resolve<CandidateExploreViewModel>() as ViewModelBase;
        }

        private async Task MasterMenuItemTappedAsync(object item)
        {
            if (item != null)
            {
                (Application.Current.MainPage as MasterDetailPage).IsPresented = false;
                switch ((item as Views.Main.MenuItem).Title)
                {
                    case "Profile":
                        OpenMainPageAsync();
                        break;
                    case "Account Setting":
                        await OpenChangePasswordPage();
                        break;
                    case "Sign out":
                        OpenSignoutPage();
                        break;
                }
            }
        }
        private async void RenderTabContent(object obj)
        {
            if (obj is int index)
            {
                TabHeaderMode = TabDisplayMode.ImageWithText;
                switch (index)
                {
                    case 0: //Profile Tab
                        if (!this.IsProfilePageRendered)
                        {
                            await ProfilePage.InitializeAsync(null);
                        }
                        break;
                    case 1: //Job Tab
                        if (!this.IsJobPageRendered)
                        {
                            await JobPage.InitializeAsync(null);
                        }
                        break;
                    case 2: //Explore Tab
                        if (!this.IsExplorePageRendered)
                        {
                            await ExplorePage.InitializeAsync(null);
                        }
                        break;
                }
            }
        }

        private async Task OpenChangePasswordPage()
        {
            _dialogService.CloseAllPopup();
            await _navigationService.NavigateToPopupAsync<AccountSettingViewModel>(true);
        }

        private async void OpenSignoutPage()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            Dictionary<string, object> obj = await _authenticationService.Logout();
            if (obj != null)
            {
                try
                {
                    if (obj["Success"].ToString() == "true") //success
                    {
                        await _dialogService.PopupMessage("Logout Successefully", "#52CD9F", "#FFFFFF");
                        await PopupNavigation.Instance.PopAllAsync();
                        App.UserName = "";
                        App.ContactID = null;
                        RequestService.ACCESS_TOKEN = "";
                        await _navigationService.NavigateToAsync<LoginViewModel>();
                    }
                    else if (obj["Success"].ToString() == "false")
                    {
                        if (obj["Message"].ToString() == "Fail")
                        {
                            await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                        }
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                }
            }
            await _dialogService.CloseLoadingPopup(pop);
        }

        private void OpenMainPageAsync()
        {
            CandidateMainViewModel.Current.SelectTab(CandidateTab.Profile);
            //await ProfilePage.InitializeAsync(null);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await ProfilePage.InitializeAsync(null);
            AvatarUrl = RequestService.HOST_NAME + "api/Document/GetContactProfileImageByContactID?id=" + App.ContactID.ToString();
            UserName = App.UserName;
        }

        public void SelectTab(CandidateTab tab)
        {
            this.SelectedIndex = (int)tab;
        }
    }

    public enum CandidateTab
    {
        Profile = 0,
        Job = 1,
        Explore = 2,
        Notify = 3,
        Message = 4
    }
}
