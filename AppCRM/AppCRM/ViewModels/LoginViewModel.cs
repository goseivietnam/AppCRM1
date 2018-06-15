using AppCRM.ViewModels.Base;
using AppCRM.Services.Authentication;
using AppCRM.Utils;
using System.Threading.Tasks;
using System.Windows.Input;
using AppCRM.Services.Dialog;
using AppCRM.Views;
using Rg.Plugins.Popup.Services;
using AppCRM.ViewModels;
using AppCRM.Services.Request;
using AppCRM.ViewModels.Main.Candidate;
using AppCRM.Services.Navigation;

namespace AppCRM.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private string _userName = "thuleduy01@gmail.com";
        private string _password = "12345";

        public LoginViewModel(IAuthenticationService authenticationService, IDialogService dialogService,INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _navigationService = navigationService;
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

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public ICommand SignInCommand => new AsyncCommand(SignInAsync);

        public ICommand RegisterCommand => new AsyncCommand(RegisterPopupAsync);

        private async Task SignInAsync()
        {
            IsBusy = true;
            var y = await _authenticationService.LoginAsync(UserName, Password);
            if (y != null)
            {
                if (y["Success"] == "true")
                {
                await _dialogService.PopupMessage("Login Successefully", "#52CD9F", "#FFFFFF");
                    IsBusy = false;
                    if (y["Roles"] == "Employer")
                    {
                    }
                    else if (y["Roles"] == "Candidate")
                    {
                    App.UserID = y["ContactID"];
                    RequestService.ACCESS_TOKEN = y["access_token"];
                    await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                    }
              
               
                }
                else if (y["Message"] == "IsActive") //success //fail
                {
                    await _dialogService.PopupMessage("This account yet active!", "#CF6069", "#FFFFFF");
                }
                else if (y["Message"] == "IsRequireReset") //success //fail
                {
                    await _dialogService.PopupMessage("This account need reset!", "#CF6069", "#FFFFFF");
                }
                else if (y["Message"] == "LoginFail") //success //fail
                {
                    await _dialogService.PopupMessage("Login fail, please try again!", "#CF6069", "#FFFFFF");
                }
            }
            else
            {
                await _dialogService.PopupMessage("Login fail, please try again!", "#CF6069", "#FFFFFF");
                await NavigationService.NavigateToAsync<LoginViewModel>();
            }
            IsBusy = false;
        }

        private async Task RegisterPopupAsync()
        {
            var popup = new RegisterPopupPage();
            var viewModel = Locator.Instance.Resolve<RegisterPopupViewModel>() as ViewModelBase;
            popup.BindingContext = viewModel;
            await PopupNavigation.Instance.PushAsync(popup);
        }

    }
}
