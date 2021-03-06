﻿using AppCRM.Services.Authentication;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private string _userName = App.UserName;
        private string _password = App.PassWord;

        public LoginViewModel(IAuthenticationService authenticationService, IDialogService dialogService, INavigationService navigationService)
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

        private async Task SignInAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            Dictionary<string, object> obj = await _authenticationService.LoginAsync(UserName, Password);
            if (obj != null)
            {
                if (obj["Success"].ToString() == "true")
                {
                    await _dialogService.CloseLoadingPopup(pop);
                    await _dialogService.PopupMessage("Login Successefully", "#52CD9F", "#FFFFFF");

                    if (obj["Roles"].ToString() == "Employer")
                    {
                    }
                    else if (obj["Roles"].ToString() == "Candidate")
                    {
                        App.ContactID = obj["ContactID"].ToString();
                        App.UserName = obj["UserName"].ToString();
                        App.PassWord = Password;
                        RequestService.ACCESS_TOKEN = obj["access_token"].ToString();
                        await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                    }
                }
                else if (obj["Message"].ToString() == "IsActive") //success //fail
                {
                    await _dialogService.PopupMessage("This account yet active!", "#CF6069", "#FFFFFF");
                }
                else if (obj["Message"].ToString() == "IsRequireReset") //success //fail
                {
                    await _dialogService.PopupMessage("This account need reset!", "#CF6069", "#FFFFFF");
                }
                else if (obj["Message"].ToString() == "LoginFail") //success //fail
                {
                    await _dialogService.PopupMessage("Login fail, please try again!", "#CF6069", "#FFFFFF");
                }
            }
            else
            {
                await _dialogService.PopupMessage("Login fail, please try again!", "#CF6069", "#FFFFFF");
                await NavigationService.NavigateToAsync<LoginViewModel>();
            }
            await _dialogService.CloseLoadingPopup(pop);
        }

        public ICommand RegisterCommand => new AsyncCommand(RegisterPopupAsync);

        private async Task RegisterPopupAsync()
        {
            await _navigationService.NavigateToPopupAsync<RegisterPopupViewModel>(true);
        }

    }
}
