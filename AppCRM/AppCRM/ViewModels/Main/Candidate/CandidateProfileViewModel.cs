using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Navigation;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.Validations;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate.Profile;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateProfileViewModel : ViewModelBase
    {

        private readonly ICandidateDetailsService _candidateDetailService;
        private readonly INavigationService _navigationService;
        private CandidateProfile _profile;

        // height listview 
        private int _contactLinksListViewHeightRequest;
        private int _educationListViewHeightRequest;
        private int _workExperienceListViewHeightRequest;
        private int _skillListViewHeightRequest;
        private int _qualificationListViewHeightRequest;
        private int _licenceListViewHeightRequest;
        private int _documentListViewHeightRequest;
        private int _referenceListViewHeightRequest;
        //private string _avatarUrl;
        public CandidateProfileViewModel(ICandidateDetailsService candidateDetailService,INavigationService navigationService)
        {
            _candidateDetailService = candidateDetailService;
            _navigationService = navigationService;
        }


        public CandidateProfile Profile
        {
            get
            {
                return _profile;
            }
            set
            {
                _profile = value;
                OnPropertyChanged();
            }
        }

        public int ContactLinksListViewHeightRequest
        {
            get
            {
                return _contactLinksListViewHeightRequest;
            }
            set
            {
                _contactLinksListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int EducationListViewHeightRequest
        {
            get
            {
                return _educationListViewHeightRequest;
            }
            set
            {
                _educationListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int WorkExperienceListViewHeightRequest
        {
            get
            {
                return _workExperienceListViewHeightRequest;
            }
            set
            {
                _workExperienceListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int SkillListViewHeightRequest
        {
            get
            {
                return _skillListViewHeightRequest;
            }
            set
            {
                _skillListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int QualificationListViewHeightRequest
        {
            get
            {
                return _qualificationListViewHeightRequest;
            }
            set
            {
                _qualificationListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int LicenceListViewHeightRequest
        {
            get
            {
                return _licenceListViewHeightRequest;
            }
            set
            {
                _licenceListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int DocumentListViewHeightRequest
        {
            get
            {
                return _documentListViewHeightRequest;
            }
            set
            {
                _documentListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int ReferenceListViewHeightRequest
        {
            get
            {
                return _referenceListViewHeightRequest;
            }
            set
            {
                _referenceListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }

        public ICommand masterPageBtnCommand => new Command(masterPageBtnAsync);
        public ICommand BtnAddEducationCommand => new AsyncCommand(BtnAddEducationCommandAsync);
        public ICommand BtnAddWorkExperienceCommand => new AsyncCommand(BtnAddWorkExperienceCommandAsync);
        public ICommand BtnAddSkillCommand => new AsyncCommand(BtnAddSkillCommandAsync);
        public ICommand BtnAddQualificationCommand => new AsyncCommand(BtnAddQualificationCommandAsync);
        public ICommand BtnAddLicenceCommand => new AsyncCommand(BtnAddLicenceCommandAsync);
        public ICommand BtnAddDocumentCommand => new AsyncCommand(BtnAddDocumentCommandAsync);
        public ICommand BtnAddReferenceCommand => new AsyncCommand(BtnAddReferenceCommandAsync);
        public ICommand BtnEditProfileCommand => new AsyncCommand(BtnEditProfileCommandAsync);
        public ICommand ListViewCommand => new Command(ListView_ItemTapped);

        private void masterPageBtnAsync()
        {
            (Application.Current.MainPage as MasterDetailPage).IsPresented = true;
        }

        private async Task BtnAddEducationCommandAsync()
        {
            await _navigationService.NavigateToPopupAsync<AddEducationViewModel>(true);
        }

        private async Task BtnAddWorkExperienceCommandAsync()
        {
            await _navigationService.NavigateToPopupAsync<AddWorkExprienceViewModel>(true);
        }

        private async Task BtnAddSkillCommandAsync()
        {
            await _navigationService.NavigateToPopupAsync<AddSkillPageViewModel>(true);
        }

        private async Task BtnAddQualificationCommandAsync()
        {
            await _navigationService.NavigateToPopupAsync<AddQualificationViewModel>(true);
        }

        private async Task BtnAddLicenceCommandAsync()
        {
            await _navigationService.NavigateToPopupAsync<AddLicenceViewModel>(true);
        }

        private async Task BtnAddDocumentCommandAsync()
        {
            await _navigationService.NavigateToPopupAsync<AddDocumentViewModel>(true);
        }

        private async Task BtnAddReferenceCommandAsync()
        {
            await _navigationService.NavigateToPopupAsync<AddReferenceViewModel>(true);
        }

        private async Task BtnEditProfileCommandAsync()
        {
            await _navigationService.NavigateToPopupAsync<EditProfileViewModel>(true);
        }

        private void ListView_ItemTapped()
        {
           
        }
        #region Initdata

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            var contactID = App.ContactID;
            dynamic obj = await _candidateDetailService.GetEmployerCandidateDetail();
            //Get all list
            List<ContactEducation> educationList = JsonConvert.DeserializeObject<List<ContactEducation>>(obj["Educations"].ToString());
            List<ContactWorkExprience> workExprienceList = JsonConvert.DeserializeObject<List<ContactWorkExprience>>(obj["WorkExperiences"].ToString());
            List<ContactSkill> skillList = JsonConvert.DeserializeObject<List<ContactSkill>>(obj["Skills"].ToString());
            List<ContactQualification> qualificationList = JsonConvert.DeserializeObject<List<ContactQualification>>(obj["Qualifications"].ToString());
            List<ContactLicence> licenceList = JsonConvert.DeserializeObject<List<ContactLicence>>(obj["Licences"].ToString());
            List<Document> documentList = JsonConvert.DeserializeObject<List<Document>>(obj["Documents"].ToString());
            List<ContactReference> referenceList = JsonConvert.DeserializeObject<List<ContactReference>>(obj["References"].ToString());
            List<InterestedRole> roles = JsonConvert.DeserializeObject<List<InterestedRole>>(obj["Role"].ToString());
            List<ContactLink> contactLinks = new List<ContactLink>();
            contactLinks.Add(new ContactLink
            {
                Title = obj["CandidateDetails"]["Email"],
                IconSource = "icon_email.png"
            });
            contactLinks.Add(new ContactLink
            {
                Title = obj["CandidateDetails"]["Phone"],
                IconSource = "icon_phone.png"
            });
            contactLinks.Add(new ContactLink
            {
                Title = obj["CandidateDetails"]["CityName"],
                IconSource = "icon_address.png"
            });

            string interestedRoles = "";
            for (var i = 0; i < roles.Count; i++)
            {
                interestedRoles += roles[i].Name + ", ";
            }
            interestedRoles = interestedRoles.Substring(0, interestedRoles.Length - 2);

            Profile = new CandidateProfile()
            {
                UserID = contactID,
                AvatarUrl = RequestService.HOST_NAME + "api/Document/GetContactImage?id=" + obj["CandidateDetails"]["ProfileImage"],
                CoverUrl = RequestService.HOST_NAME + "api/Document/GetContactImage?id=" + obj["CandidateDetails"]["CoverImage"],
                FullName = obj["CandidateDetails"]["FullName"],
                RoleAndAddress = interestedRoles + " - " + obj["CandidateDetails"]["CityName"],
                Greeting = "Hi, I am " + obj["CandidateDetails"]["FullName"],
                Introduction = Utilities.HtmlToPlainText(obj["CandidateDetails"]["AboutMe"].ToString()),
                Educations = educationList,
                WorkExpriences = workExprienceList,
                Skills = skillList,
                Qualifications = qualificationList,
                Licences = licenceList,
                Documents = documentList,
                References = referenceList,
                ContactLinks = contactLinks
            };

            ContactLinksListViewHeightRequest = (contactLinks.Count * 28 - 1);
            EducationListViewHeightRequest = (educationList.Count * 91 - 1 * (educationList.Count > 1 ? 1 : 0));
            WorkExperienceListViewHeightRequest = (workExprienceList.Count * 91 - 1 * (workExprienceList.Count > 1 ? 1 : 0));
            SkillListViewHeightRequest = (skillList.Count * 31 - 1 * (skillList.Count > 1 ? 1 : 0));
            QualificationListViewHeightRequest = (qualificationList.Count * 91 - 1 * (qualificationList.Count > 1 ? 1 : 0));
            LicenceListViewHeightRequest = (licenceList.Count * 61 - 1 * (licenceList.Count > 1 ? 1 : 0));
            DocumentListViewHeightRequest = (documentList.Count * 31 - 1 * (documentList.Count > 1 ? 1 : 0));
            ReferenceListViewHeightRequest = (referenceList.Count * 91 - 1 * (referenceList.Count > 1 ? 1 : 0));
            IsBusy = false;
        }

        #endregion
    }
}