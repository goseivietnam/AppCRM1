using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels.Account
{
    public class CandidateRegisterViewModel:ViewModelBase
    {
        private readonly IDialogService _dialogService; 
        private readonly ICandidateDetailsService _candidateDetailsService;

        private string _fieldFirstName;
        private string _fieldLastName;
        private string _fieldEmail;
        private string _fieldPassword;
        private string _fieldPasswordConfirm;
        private string _fieldJobInterest;
        private string _fieldJobLocation;
         
        public CandidateRegisterViewModel(IDialogService dialogService,ICandidateDetailsService candidateDetailsService)
        {
            _dialogService = dialogService;
            _candidateDetailsService = candidateDetailsService;
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
        public ICommand BtnCancelCommand => new AsyncCommand(BtnCancelAsync);

        private async Task SubmitRegisterAsync()
        {
            IsBusy = true;
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

            var obj = await _candidateDetailsService.CandidateRegister(reg);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _dialogService.PopupMessage("Register Successefully", "#52CD9F", "#FFFFFF");
                        App.UserID = obj["ContactID"];
                        RequestService.ACCESS_TOKEN = obj["access_token"];
                    }
                    else if (obj["Message"] == "IsExists") //is exists
                    {
                        await _dialogService.PopupMessage("This account is exists!", "#CF6069", "#FFFFFF");
                    }
                    else if (obj["Message"] == "TryAgaint") //fail
                    {
                        await _dialogService.PopupMessage("An error has occurred, please try again!", "#CF6069", "#FFFFFF");
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!", "#CF6069", "#FFFFFF");
                }
            }

            IsBusy = false;
        }
        private async Task BtnCancelAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
