using AppCRM.Controls;
using AppCRM.Extensions;
using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Account
{
    public class CandidateRegisterViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateDetailsService _candidateDetailsService;
        private readonly INavigationService _navigationService;

        private string _fieldFirstName;
        private string _fieldLastName;
        private string _fieldEmail;
        private string _fieldPassword;
        private string _fieldPasswordConfirm;
        private string _fieldJobInterest;
        private string _fieldJobLocation;
        private ImageSource _profileAvatarSource;
        private ObservableCollection<PickerItem> _jobInterestCollection;
        private ObservableCollection<PickerItem> _jobLocationCollection;

        private SJFileStream _avatarStream = null;

        public CandidateRegisterViewModel(IDialogService dialogService, ICandidateDetailsService candidateDetailsService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateDetailsService = candidateDetailsService;
            _navigationService = navigationService;
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
        public ObservableCollection<PickerItem> JobInterestCollection
        {
            get { return _jobInterestCollection; }
            set { _jobInterestCollection = value; OnPropertyChanged(); }
        }
        public ObservableCollection<PickerItem> JobLocationCollection
        {
            get { return _jobLocationCollection; }
            set { _jobLocationCollection = value; OnPropertyChanged(); }
        }

        public ICommand SubmitRegisterCommand => new AsyncCommand(SubmitRegisterAsync);
        public ICommand BtnCancelCommand => new AsyncCommand(BtnCancelAsync);
        public ICommand PickAvatarBtnCommand => new AsyncCommand(PickAvatar);
        public ICommand JobInterestChangeCommand => new Command(UpdateJobInterest);
        public ICommand JobLocationChangeCommand => new Command(UpdateJobLocation);
        
        private async Task SubmitRegisterAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            Register reg = new Register
            {
                FirstName = _fieldFirstName,
                LastName = _fieldLastName,
                Email = _fieldEmail,
                Password = _fieldPassword,
                UserName=_fieldEmail,
                ConfirmPassword = _fieldPasswordConfirm,
                InterestedRoleIds = _fieldJobInterest,
                InterestedLocationIds = _fieldJobLocation
            };

            var obj = await _candidateDetailsService.CandidateRegister(reg);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _candidateDetailsService.AddEditContactAvatarImage(_avatarStream);
                        if (obj["Roles"] == "Employer")
                        {
                        }
                        else if (obj["Roles"] == "Candidate")
                        {
                            await _dialogService.PopupMessage("Register Successefully", "#52CD9F", "#FFFFFF");
                            App.ContactID = obj["ContactID"];
                            App.UserName = obj["UserName"];
                            App.PassWord = FieldPassword;
                            RequestService.ACCESS_TOKEN = obj["access_token"];
                            await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                            await PopupNavigation.Instance.PopAllAsync();
                        }
                       
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

            await _dialogService.CloseLoadingPopup(pop);
        }
        private async Task BtnCancelAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        private async Task PickAvatar()
        {
            var pop =await _dialogService.OpenLoadingPopup();
            _avatarStream = await DependencyService.Get<IFilePicker>().GetImageStreamAsync();
            if (_avatarStream != null && _avatarStream.Stream != null)
            {
                ProfileAvatarSource = ImageSource.FromStream(() => _avatarStream.Stream);
            }
            await _dialogService.CloseLoadingPopup(pop);
        }
        private void UpdateJobInterest(object selectedValues)
        {
            _fieldJobInterest = String.Join(",", (selectedValues as List<string>).ToArray());
        }
        private void UpdateJobLocation(object selectedValues)
        {
            _fieldJobLocation = String.Join(",", (selectedValues as List<string>).ToArray());
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var InterestedRolesDDL = await _candidateDetailsService.GetInterestedRolesDDL();
            JobInterestCollection = InterestedRolesDDL.ToObservableCollection();
            var InterestedLocationsDDL = await _candidateDetailsService.GetInterestedLocationsDDL();
            JobLocationCollection = InterestedLocationsDDL.ToObservableCollection();
        }

    }
}
