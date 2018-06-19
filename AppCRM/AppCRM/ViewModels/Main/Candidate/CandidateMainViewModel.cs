using AppCRM.Services.Authentication;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.Views.Main.Candidate.ProfilePage;
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

        private CandidateProfilePage _profilePage;
        private ContentPage _jobPage;
        private ContentPage _explorePage;
        private ContentPage _notifyPage;
        private ContentPage _messagePage;

        public CandidateProfilePage ProfilePage
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
        public ContentPage JobPage
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
        public ContentPage ExplorePage
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
        public ContentPage NotifyPage
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
        public ContentPage MessagePage
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

        public ICommand MasterMenuItemTappedCommand => new AsyncCommand(MasterMenuItemTappedAsync);

        public CandidateMainViewModel()
        {
            _authenticationService = Locator.Instance.Resolve<IAuthenticationService>();
            _dialogService = Locator.Instance.Resolve<IDialogService>();
            _navigationService = Locator.Instance.Resolve<INavigationService>();

            InitCandidateProfilePage();

            //Initializing JobPage
            JobPage = new ContentPage();

            //Initializing ExplorePage
            ExplorePage = new ContentPage();

            //Initializing NotifyPage
            NotifyPage = new ContentPage();

            //Initializing MessagePage
            MessagePage = new ContentPage();

        }

        private async Task MasterMenuItemTappedAsync(object item)
        {
            if (item != null)
            {
                switch ((item as Views.Main.MenuItem).Title)
                {
                    case "Profile":
                        OpenMainPage();
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

        private void InitCandidateProfilePage()
        {
            var profilePage = new CandidateProfilePage();
            profilePage.BindingContext = Locator.Instance.Resolve<CandidateProfileViewModel>() as ViewModelBase;
            (profilePage.BindingContext as ViewModelBase).InitializeAsync(profilePage);
            ProfilePage = profilePage;
        }

        private async Task OpenChangePasswordPage()
        {
            //_dialogService.CloseAllPopup();
            //await _navigationService.NavigateToPopupAsync<RegisterPopupViewModel>(true);
        }

        private async void OpenSignoutPage()
        {
            //var obj = await _authenticationService.Logout();
            //if (obj != null)
            //{
            //    try
            //    {
            //        if (obj["Success"] == "true") //success
            //        {
            //            await PopupMessage("Logout Successefully", "#52CD9F", "#FFFFFF");
            //            App.UserName = "";
            //            App.ContactID = null;
            //            DataService.ACCESS_TOKEN = "";

            //            LoginPage loginPage = new LoginPage();
            //            NavigationPage.SetHasNavigationBar(loginPage, false);
            //            Application.Current.MainPage = new NavigationPage(loginPage);
            //        }
            //        else if (obj["Success"] == "false")
            //        {
            //            if (obj["Message"] == "Fail")
            //            {
            //                await PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
            //            }
            //            else if (obj["Message"] == "AttachFail")
            //            {
            //                await PopupMessage("Attach file Fail, please try again!!", "#CF6069", "#FFFFFF");
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        await PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
            //    }
            //}
        }

        private void OpenMainPage()
        {
            //CandidateMainPage candidateMainPage = new CandidateMainPage();
            //NavigationPage.SetHasNavigationBar(candidateMainPage, false);
            //Application.Current.MainPage = new NavigationPage(candidateMainPage);
        }
    }
}
