using AppCRM.Controls;
using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Services.Request;
using AppCRM.Tools;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Profile
{
    public class EditProfileViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateDetailsService _candidateDetailsService;
        private readonly INavigationService _navigationService;

        private string _avatarUrl;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _address;
        private string _cityName;
        private ObservableCollection<PickerItem> _nationalityDDL;
        private PickerItem _nationalitySelected;
        private DateTime? _birthDay;
        private string _aboutMe;
        private string _coverUrl;
        private bool _btnUploadResumIsEnable = true;
        private bool _btnPickAvatarIsEnable = true;
        private bool _btnEditCoverIsEnable = true;
        private string _fileName;
        private bool _fileNameIsVisible = false;
        private bool _fileAttachImageIsVisible = false;
        private SJFileStream stream;

        public EditProfileViewModel(IDialogService dialogService, ICandidateDetailsService candidateDetailsService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateDetailsService = candidateDetailsService;
            _navigationService = navigationService;
        }

        public string AvatarUrl
        {
            get
            {
                return _avatarUrl;
            }
            set
            {
                _avatarUrl = value;
                OnPropertyChanged();
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
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
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
        public string CityName
        {
            get
            {
                return _cityName;
            }
            set
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<PickerItem> NationalityDDL
        {
            get
            {
                return _nationalityDDL;
            }
            set
            {
                _nationalityDDL = value;
                OnPropertyChanged();
            }
        }

        public PickerItem NationalitySelected
        {
            get
            {
                return _nationalitySelected;
            }
            set
            {
                _nationalitySelected = value;
                OnPropertyChanged();
            }
        }
        public DateTime? BirthDay
        {
            get
            {
                return _birthDay;
            }
            set
            {
                _birthDay = value;
                OnPropertyChanged();
            }
        }
        public string AboutMe
        {
            get
            {
                return _aboutMe;
            }
            set
            {
                _aboutMe = value;
                OnPropertyChanged();
            }
        }
        public string CoverUrl
        {
            get
            {
                return _coverUrl;
            }
            set
            {
                _coverUrl = value;
                OnPropertyChanged();
            }
        }
        public bool BtnUploadResumIsEnable
        {
            get
            {
                return _btnUploadResumIsEnable;
            }
            set
            {
                _btnUploadResumIsEnable = value;
                OnPropertyChanged();
            }
        }
        public bool BtnPickAvatarIsEnable
        {
            get
            {
                return _btnPickAvatarIsEnable;
            }
            set
            {
                _btnPickAvatarIsEnable = value;
                OnPropertyChanged();
            }
        }
        public bool BtnEditCoverIsEnable
        {
            get
            {
                return _btnEditCoverIsEnable;
            }
            set
            {
                _btnEditCoverIsEnable = value;
                OnPropertyChanged();
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }
        public bool FileNameIsVisible
        {
            get
            {
                return _fileNameIsVisible;
            }
            set
            {
                _fileNameIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool FileAttachImageIsVisible
        {
            get
            {
                return _fileAttachImageIsVisible;
            }
            set
            {
                _fileAttachImageIsVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand BtnSaveProfileCommand => new AsyncCommand(BtnSaveProfileCommandAsync);
        public ICommand BtnUploadResumCommand => new AsyncCommand(BtnUploadResumCommandAsync);
        public ICommand BtnPickAvatarCommand => new AsyncCommand(BtnPickAvatarCommandAsync);
        public ICommand BtnEditCoverCommand => new AsyncCommand(BtnEditCoverCommandAsync);

        private async Task BtnBackAsync()
        {
            var result = await _dialogService.Alert("You have unsaved change", "Do you want discard your changes?", "Discard", "Cancel");
            if (result)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private async Task BtnSaveProfileCommandAsync()
        {
            IsBusy = true;
            DateTime? birthday = null;
            try
            {
                birthday = _birthDay;
            }
            catch
            {
                birthday = null;
            }


            CandidateProfile profile = new CandidateProfile
            {
                FirstName = _fileName,
                LastName = _lastName,
                Email = _email,
                Address = _address,
                CityName = _cityName,
                Nationality = _nationalitySelected.Name,
                NationalityID = _nationalitySelected.Id,
                DateOfBirth = _birthDay,
                AboutMe = _aboutMe,
            };
            var obj = await _candidateDetailsService.EditCandidateDetails(profile);
            IsBusy = false;

            if (obj != null)
            {
                try
                {
                    await _dialogService.PopupMessage("Add new Work Exprience Successefully", "#52CD9F", "#FFFFFF");
                    IsBusy = true;
                    var objupload = await _candidateDetailsService.UploadResume(stream);
                    IsBusy = false;

                    if (objupload != null)
                    {
                        try
                        {
                            if (objupload["Success"] == "true") //success
                            {
                                await _dialogService.PopupMessage("Upload Resume Successefully", "#52CD9F", "#FFFFFF");
                                await PopupNavigation.Instance.PopAllAsync();
                                await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                            }
                            else if (objupload["Success"] == "false")
                            {
                                if (objupload["Message"] == "Fail")
                                {
                                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                                }
                                else if (objupload["Message"] == "NodocumentFile")
                                {
                                    await _dialogService.PopupMessage("Attach file Fail, please try again!!", "#CF6069", "#FFFFFF");
                                }
                            }
                        }
                        catch
                        {
                            await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                            IsBusy = false;
                        }
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                }
            }
        }

        private async Task BtnUploadResumCommandAsync()
        {
            BtnUploadResumIsEnable = false;
            stream = await DependencyService.Get<IFilePicker>().GetFileStreamAsync(Tools.Enum.FileTypeDocAndPdf);
            BtnUploadResumIsEnable = true;
            FileName = stream.FileName;
            FileNameIsVisible = true;
            FileAttachImageIsVisible = true;
        }

        private async Task BtnPickAvatarCommandAsync()
        {
            BtnPickAvatarIsEnable = false;
            SJFileStream stream = await DependencyService.Get<IFilePicker>().GetImageStreamAsync();
            BtnPickAvatarIsEnable = true;

            IsBusy = true;
            var obj = await _candidateDetailsService.AddEditContactAvatarImage(stream);
            IsBusy = false;

            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        _avatarUrl = RequestService.HOST_NAME + "api/Document/GetContactImage?id=" + obj["Result"];
                        await _dialogService.PopupMessage("Update Cover image Successefully", "#52CD9F", "#FFFFFF");
                    }
                    else if (obj["Success"] == "false")
                    {
                        await _dialogService.PopupMessage("Haven't image file, please try again!!", "#CF6069", "#FFFFFF");
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                }
            }
        }

        private async Task BtnEditCoverCommandAsync()
        {
            BtnEditCoverIsEnable = false;
            stream = await DependencyService.Get<IFilePicker>().GetFileStreamAsync(Tools.Enum.FileTypeDocAndPdf);
            BtnEditCoverIsEnable = true;
            IsBusy = true;
            var obj = await _candidateDetailsService.AddEditContactCoverImage(stream);
            IsBusy = false;

            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true")
                    {
                        _coverUrl = RequestService.HOST_NAME + "api/Document/GetContactImage?id=" + obj["Result"];
                        await _dialogService.PopupMessage("Update Cover image Successefully", "#52CD9F", "#FFFFFF");
                    }
                    else if (obj["Success"] == "false")
                    {
                        await _dialogService.PopupMessage("Haven't image file, please try again!!", "#CF6069", "#FFFFFF");
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                }
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            //load profile data from DataService
            var obj = await _candidateDetailsService.GetEmployerCandidateProfile();
            DateTime? datebirth;
            try
            {
                datebirth = DateTime.Parse(obj["CandidateDetails"]["DateOfBirth"].ToString());
            }
            catch
            {
                datebirth = null;
            }
            ObservableCollection<PickerItem> nationalityDDL = JsonConvert.DeserializeObject<ObservableCollection<PickerItem>>(obj["NationalityDLL"].ToString());
            Guid? CurrentNationalityID = obj["CandidateDetails"]["NationalityID"] != null ? (Guid?)new Guid(obj["CandidateDetails"]["NationalityID"].ToString()) : null;
            PickerItem CurrentNationality = nationalityDDL.Where(x => x.Id == CurrentNationalityID).FirstOrDefault();

            FirstName = obj["CandidateDetails"]["FirstName"];
            LastName = obj["CandidateDetails"]["LastName"];
            Email = obj["CandidateDetails"]["Email"];
            Address = obj["CandidateDetails"]["Address"];
            CityName = obj["CandidateDetails"]["CityName"];
            NationalityDDL = nationalityDDL;
            NationalitySelected = CurrentNationality;
            BirthDay = datebirth;
            AboutMe = Utilities.HtmlToPlainText(obj["CandidateDetails"]["AboutMe"].ToString());
            AvatarUrl = RequestService.HOST_NAME + "api/Document/GetContactImage?id=" + obj["CandidateDetails"]["ProfileImage"];
            CoverUrl = RequestService.HOST_NAME + "api/Document/GetContactImage?id=" + obj["CandidateDetails"]["CoverImage"];

            IsBusy = false;
        }
    }
}
