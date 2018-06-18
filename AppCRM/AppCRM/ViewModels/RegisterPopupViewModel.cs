﻿using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Account;
using AppCRM.ViewModels.Base;
using AppCRM.Views.Account;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels
{
    public class RegisterPopupViewModel:ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public RegisterPopupViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
        }

        public ICommand CandidateRegisterCommand => new AsyncCommand(CandidateRegisterAsync);
        public ICommand EmployerRegisterCommand => new AsyncCommand(EmployerRegisterrAsync);


        private async Task CandidateRegisterAsync()
        {
            await _navigationService.NavigateToPopupAsync<CandidateRegisterViewModel>(true);
        }


        private async Task EmployerRegisterrAsync()
        {
            await _navigationService.NavigateToPopupAsync<EmployerRegisterViewModel>(true);
        }
       
    }
}