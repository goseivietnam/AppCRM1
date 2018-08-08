using AppCRM.Controls;
using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Employer;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Account
{
    public class EmployerRegisterViewModel:ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IEmployerDetailService _employerDetailService;
        private readonly ICandidateDetailsService _candidateDetailService;

        private string _fieldFirstName;
        private string _fieldLastName;
        private string _fieldEmail;
        private string _fieldCompanyName;
        private string _fieldIndustry;
        private string _fieldPassword;
        private string _fieldPasswordConfirm;
        private ImageSource _profileAvatarSource;

        private SJFileStream _avatarStream = null;

        public EmployerRegisterViewModel(IDialogService dialogService, IEmployerDetailService EmployerDetailService,ICandidateDetailsService CandidateDetailService)
        {
            _dialogService = dialogService;
            _employerDetailService = EmployerDetailService;
            _candidateDetailService = CandidateDetailService;
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
            var pop = await _dialogService.OpenLoadingPopup();
            Register reg = new Register
            {
                FirstName = _fieldFirstName,
                LastName = _fieldLastName,
                Email = _fieldEmail,
                Password = _fieldPassword,
                UserName = _fieldEmail,
                ConfirmPassword = _fieldPasswordConfirm,
                AccountName = _fieldCompanyName,
                Industry = _fieldIndustry
            };

            Dictionary<string, object> obj = await _employerDetailService.EmployerRegister(reg);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"].ToString() == "true") //success
                    {
                        await _dialogService.PopupMessage("Register Successefully", "#52CD9F", "#FFFFFF");
                        App.ContactID = obj["ContactID"].ToString();
                        App.UserName = obj["UserName"].ToString();
                        App.PassWord = FieldPassword;
                        RequestService.ACCESS_TOKEN = obj["access_token"].ToString();
                        if (_avatarStream != null)
                        {
                            Dictionary<string, object> objUpload = await _candidateDetailService.AddEditContactAvatarImage(_avatarStream);
                            try
                            {
                                if (objUpload["Success"].ToString() == "true") //success
                                {
                                    await _dialogService.PopupMessage("Update Cover image Successefully", "#52CD9F", "#FFFFFF");
                                }
                                else if (objUpload["Success"].ToString() == "false")
                                {
                                    await _dialogService.PopupMessage("Haven't image file, please try again!!", "#CF6069", "#FFFFFF");
                                }
                            }
                            catch
                            {
                                await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                                await _dialogService.CloseLoadingPopup(pop);
                            }
                            finally
                            {
                                await PopupNavigation.Instance.PopAllAsync();
                                await NavigationService.NavigateToAsync<LoginViewModel>();
                            }
                        }
                        else
                        {
                            await PopupNavigation.Instance.PopAllAsync();
                            await NavigationService.NavigateToAsync<LoginViewModel>();
                        }
                    }
                    else if (obj["Message"].ToString() == "IsExists") //is exists
                    {
                        await _dialogService.PopupMessage("This account is exists!", "#CF6069", "#FFFFFF");
                    }
                    else if (obj["Message"].ToString() == "TryAgaint") //fail
                    {
                        await _dialogService.PopupMessage("An error has occurred, please try again!", "#CF6069", "#FFFFFF");
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!", "#CF6069", "#FFFFFF");
                }
            }

            await _dialogService.CloseLoadingPopup(pop);
        }
        private async Task BtnCancelAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        private async Task PickAvatar()
        {
            _avatarStream = await DependencyService.Get<IFilePicker>().GetImageStreamAsync();
            if (_avatarStream != null && _avatarStream.Stream != null)
            {
                ProfileAvatarSource = ImageSource.FromStream(() => _avatarStream.Stream);
            }
        }
    }
}
