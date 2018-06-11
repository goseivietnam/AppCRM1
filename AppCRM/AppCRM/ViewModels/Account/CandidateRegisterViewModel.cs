using AppCRM.Models;
using AppCRM.Services.Dialog;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels.Account
{
    public class CandidateRegisterViewModel:ViewModelBase
    {
        private readonly IDialogService _dialogService; 

        private string _fieldFirstName;
        private string _fieldLastName;
        private string _fieldEmail;
        private string _fieldPassword;
        private string _fieldPasswordConfirm;
        private string _fieldJobInterest;
        private string _fieldJobLocation;
         
        public CandidateRegisterViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public string FieldFirstName
        {
            get
            {
                return _fieldFirstName;
            }
            set
            {
                _fieldFirstName = value;
                OnPropertyChanged();
            }
        }
        public string FieldLastName
        {
            get
            {
                return _fieldLastName;
            }
            set
            {
                _fieldLastName = value;
                OnPropertyChanged();
            }
        }
        public string FieldEmail
        {
            get
            {
                return _fieldEmail;
            }
            set
            {
                _fieldEmail = value;
                OnPropertyChanged();
            }
        }
        public string FieldPassword
        {
            get
            {
                return _fieldPassword;
            }
            set
            {
                _fieldPassword = value;
                OnPropertyChanged();
            }
        }
        public string FieldPasswordConfirm
        {
            get
            {
                return _fieldPasswordConfirm;
            }
            set
            {
                _fieldPasswordConfirm = value;
                OnPropertyChanged();
            }
        }
        public string FieldJobInterest
        {
            get
            {
                return _fieldJobInterest;
            }
            set
            {
                _fieldJobInterest = value;
                OnPropertyChanged();
            }
        }
        public string FieldJobLocation
        {
            get
            {
                return _fieldJobLocation;
            }
            set
            {
                _fieldJobLocation = value;
                OnPropertyChanged();
            }
        }


        public ICommand SubmitRegisterCommand => new AsyncCommand(SubmitRegisterAsync);
      //  public ICommand SignInCommand => new AsyncCommand(SignInAsync);

        private async Task SubmitRegisterAsync()
        {
            Register reg = new Register
            {
                FirstName = _fieldFirstName,
                LastName = _fieldLastName,
                Email = _fieldEmail,
                Password = _fieldPassword,
                ConfirmPassword = _fieldPasswordConfirm,
                JobInterest = _fieldJobInterest,
                JobLocation = _fieldJobLocation
            };
            await _dialogService.PopupMessage("Register Successefully", "#52CD9F", "#FFFFFF");
        }
    }
}
