using AppCRM.ViewModels.Base;
using AppCRM.Services.Authentication;
using AppCRM.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        private string _userName;
        private string _password;

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

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

        private async Task SignInAsync()
        {
            IsBusy = true;
            var y = await _authenticationService.LoginAsync(UserName, Password);
            if (y["Success"] == "true")
            {
                await NavigationService.NavigateToAsync<MainViewModel>();
            }
            else
            {
                await NavigationService.NavigateToAsync<LoginViewModel>();
            }
            IsBusy = false;
        }
    }
}
