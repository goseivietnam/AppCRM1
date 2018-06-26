using AppCRM.Models;
using AppCRM.Services.Authentication;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Profile
{
    class AccountSettingViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private string _email;
        private string _password;
        private bool _passwordIsEnabled = false;


        public AccountSettingViewModel(IDialogService dialogService, IAuthenticationService authenticationService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _authenticationService = authenticationService;
            _navigationService = navigationService;
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
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

        public bool PasswordIsEnabled
        {
            get
            {
                return _passwordIsEnabled;
            }
            set
            {
                _passwordIsEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackCommandAsync);
        public ICommand BtnSaveAccountSettingCommand => new AsyncCommand(BtnSaveAccountSettingCommandAsync);
        public ICommand ChangePassCommand => new Command(ChangePassCommandAsync);

        private async Task BtnBackCommandAsync()
        {
            var result = await _dialogService.Alert("You have unsaved change", "Do you want discard your changes?", "Discard", "Cancel");
            if (result)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private async Task BtnSaveAccountSettingCommandAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            Contact profile = new Contact
            {
                Password = _password,
            };

            var obj = await _authenticationService.ChangePassword(profile);
            await _dialogService.CloseLoadingPopup(pop);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _dialogService.PopupMessage("Change Password Successefully", "#52CD9F", "#FFFFFF");
                        await PopupNavigation.Instance.PopAllAsync();
                        await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                    }
                    else if (obj["Success"] == "false")
                    {
                        await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                    await _dialogService.CloseLoadingPopup(pop);
                }
            }
        }

        private void ChangePassCommandAsync()
        {
            PasswordIsEnabled = true;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var pop = await _dialogService.OpenLoadingPopup();
            Email = App.UserName;
            Password = App.PassWord;
            await _dialogService.CloseLoadingPopup(pop);
        }
    }
}
