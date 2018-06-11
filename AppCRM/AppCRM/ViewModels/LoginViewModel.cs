using AppCRM.ViewModels.Base;
using AppCRM.Services.Authentication;
using AppCRM.Utils;
using System.Threading.Tasks;
using System.Windows.Input;
using AppCRM.Services.Dialog;
using AppCRM.Views;
using Rg.Plugins.Popup.Services;
using AppCRM.ViewModels;
namespace AppCRM.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private string _userName;
        private string _password;

        public LoginViewModel(IAuthenticationService authenticationService, IDialogService dialogService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
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
            if (y["Success"] == "true")
            {
                await _dialogService.PopupMessage("Login Successefully", "#52CD9F", "#FFFFFF");
                await NavigationService.NavigateToAsync<MainViewModel>();
               
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
            var viewModel = new RegisterPopupViewModel(_dialogService);
            popup.BindingContext = viewModel;
            await PopupNavigation.Instance.PushAsync(popup);
        }

    }
}
