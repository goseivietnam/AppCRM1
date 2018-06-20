using AppCRM.Controls;
using AppCRM.Models;
using AppCRM.Services.Dialog;
using AppCRM.Services.Employer;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Account
{
    public class EmployerRegisterViewModel:ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IEmployerDetailService _employerDetailService;

        private string _fieldFirstName;
        private string _fieldLastName;
        private string _fieldEmail;
        private string _fieldCompanyName;
        private string _fieldIndustry;
        private string _fieldPassword;
        private string _fieldPasswordConfirm;
        private ImageSource _profileAvatarSource;

        private SJFileStream _avatarStream = null;

        public EmployerRegisterViewModel(IDialogService dialogService, IEmployerDetailService EmployerDetailService)
        {
            _dialogService = dialogService;
            _employerDetailService = EmployerDetailService;
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
        public string FileEmail
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
        public string FieldCompanyName
        {
            get
            {
                return _fieldCompanyName;
            }
            set
            {
                _fieldCompanyName = value;
                OnPropertyChanged();
            }
        }
        public string FieldIndustry
        {
            get
            {
                return _fieldIndustry;
            }
            set
            {
                _fieldIndustry = value;
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
        public ImageSource ProfileAvatarSource
        {
            get
            {
                return _profileAvatarSource;
            }
            set
            {
                _profileAvatarSource = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitRegisterCommand => new AsyncCommand(SubmitRegisterAsync);
        public ICommand BtnCancelCommand => new AsyncCommand(BtnCancelAsync);
        public ICommand PickAvatarBtnCommand => new AsyncCommand(PickAvatar);

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
                AccountName = _fieldCompanyName,
                Industry = _fieldIndustry
            };

            var obj = await _employerDetailService.EmployerRegister(reg);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _dialogService.PopupMessage("Register Successefully", "#52CD9F", "#FFFFFF");
                        App.ContactID = obj["ContactID"];
                        App.UserName = obj["UserName"];
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
        private async Task PickAvatar()
        {
            IsBusy = true;
            _avatarStream = await DependencyService.Get<IFilePicker>().GetImageStreamAsync();
            if (_avatarStream != null && _avatarStream.Stream != null)
            {
                ProfileAvatarSource = ImageSource.FromStream(() => _avatarStream.Stream);
            }
            IsBusy = false;
        }
    }
}
