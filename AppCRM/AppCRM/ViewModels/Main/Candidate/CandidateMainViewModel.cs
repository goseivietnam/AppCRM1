using AppCRM.Services.Authentication;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate.Profile;
using AppCRM.Views.Main.Candidate.ProfilePage;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateMainViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private int _selectedIndex;
        private ViewModelBase _profilePage;
        private ViewModelBase _jobPage;
        private ViewModelBase _explorePage;
        private ViewModelBase _notifyPage;
        private ViewModelBase _messagePage;
        private string _avartarUrl;
        private string _userName;

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
        public string AvartarUrl
        {
            get
            {
                return _avartarUrl;
            }
            set
            {
                _avartarUrl = value;
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

        public ICommand MasterMenuItemTappedCommand => new AsyncCommand(MasterMenuItemTappedAsync);

        public CandidateMainViewModel()
        {
            _authenticationService = Locator.Instance.Resolve<IAuthenticationService>();
            _dialogService = Locator.Instance.Resolve<IDialogService>();
            _navigationService = Locator.Instance.Resolve<INavigationService>();

            SelectedIndex = 0;

            ProfilePage = Locator.Instance.Resolve<CandidateProfileViewModel>() as ViewModelBase;
            JobPage = Locator.Instance.Resolve<CandidateJobViewModel>() as ViewModelBase;


            ProfilePage.InitializeAsync(null);
            JobPage.InitializeAsync(null);

            //Initializing ExplorePage

            //Initializing NotifyPage

            //Initializing MessagePage

        }

        private async Task MasterMenuItemTappedAsync(object item)
        {
            if (item != null)
            {
                (Application.Current.MainPage as MasterDetailPage).IsPresented = false;
                switch ((item as Views.Main.MenuItem).Title)
                {
                    case "Profile":
                        await OpenMainPageAsync();
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

        private async Task OpenChangePasswordPage()
        {
            _dialogService.CloseAllPopup();
            await _navigationService.NavigateToPopupAsync<AccountSettingViewModel>(true);
        }

        private async void OpenSignoutPage()
        {
            var obj = await _authenticationService.Logout();
            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _dialogService.PopupMessage("Logout Successefully", "#52CD9F", "#FFFFFF");
                        await PopupNavigation.Instance.PopAllAsync();
                        App.UserName = "";
                        App.ContactID = null;
                        RequestService.ACCESS_TOKEN = "";
                        await _navigationService.NavigateToAsync<LoginViewModel>();
                    }
                    else if (obj["Success"] == "false")
                    {
                        if (obj["Message"] == "Fail")
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
        }

        private async Task OpenMainPageAsync()
        {
            SelectedIndex = 0;
            await ProfilePage.InitializeAsync(null);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            AvartarUrl = RequestService.HOST_NAME + "api/Document/GetContactImageByContactID?id=" + App.ContactID.ToString();
            UserName = App.UserName;
        }
    }
}
