using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.Validations;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate.Profile;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateProfileViewModel : ViewModelBase
    {

        private readonly ICandidateDetailsService _candidateDetailService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private CandidateProfile _profile;
        private int educationCount = 0;
        private int workExprienceCount = 0;
        private int skillCount = 0;
        private int qualificationCount = 0;
        private int licenceCount = 0;
        private int documentCount = 0;
        private int referenceCount = 0;

        private List<ContactEducation> educationList;
        private List<ContactWorkExprience> workExprienceList;
        private List<ContactSkill> skillList;
        private List<ContactQualification> qualificationList;
        private List<ContactLicence> licenceList;
        private List<Document> documentList;
        private List<ContactReference> referenceList;

        // height listview 
        private int _contactLinksListViewHeightRequest;
        private int _educationListViewHeightRequest;
        private int _workExperienceListViewHeightRequest;
        private int _skillListViewHeightRequest;
        private int _qualificationListViewHeightRequest;
        private int _licenceListViewHeightRequest;
        private int _documentListViewHeightRequest;
        private int _referenceListViewHeightRequest;

        //View More Button Text
        private string _educationViewMoreText;
        private string _workExperienceViewMoreText;
        private string _skillViewMoreText;
        private string _qualificationViewMoreText;
        private string _licenceViewMoreText;
        private string _documentViewMoreText;
        private string _referenceViewMoreText;

        private bool _educationViewMoreIsVisible = true;
        private bool _workExperienceViewMoreIsVisible = true;
        private bool _skillViewMoreIsVisible = true;
        private bool _qualificationViewMoreIsVisible = true;
        private bool _licenceViewMoreIsVisible = true;
        private bool _documentViewMoreIsVisible = true;
        private bool _referenceViewMoreIsVisible = true;

        public CandidateProfileViewModel(ICandidateDetailsService candidateDetailService, INavigationService navigationService, IDialogService dialogService)
        {
            _candidateDetailService = candidateDetailService;
            _navigationService = navigationService;
            _dialogService = dialogService;
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

        public string EducationViewMoreText
        {
            get
            {
                return _educationViewMoreText;
            }
            set
            {
                _educationViewMoreText = value;
                OnPropertyChanged();
            }
        }
        public string WorkExperienceViewMoreText
        {
            get
            {
                return _workExperienceViewMoreText;
            }
            set
            {
                _workExperienceViewMoreText = value;
                OnPropertyChanged();
            }
        }
        public string SkillViewMoreText
        {
            get
            {
                return _skillViewMoreText;
            }
            set
            {
                _skillViewMoreText = value;
                OnPropertyChanged();
            }
        }
        public string QualificationViewMoreText
        {
            get
            {
                return _qualificationViewMoreText;
            }
            set
            {
                _qualificationViewMoreText = value;
                OnPropertyChanged();
            }
        }
        public string LicenceViewMoreText
        {
            get
            {
                return _licenceViewMoreText;
            }
            set
            {
                _licenceViewMoreText = value;
                OnPropertyChanged();
            }
        }
        public string DocumentViewMoreText
        {
            get
            {
                return _documentViewMoreText;
            }
            set
            {
                _documentViewMoreText = value;
                OnPropertyChanged();
            }
        }
        public string ReferenceViewMoreText
        {
            get
            {
                return _referenceViewMoreText;
            }
            set
            {
                _referenceViewMoreText = value;
                OnPropertyChanged();
            }
        }

        public bool EducationViewMoreIsVisible
        {
            get
            {
                return _educationViewMoreIsVisible;
            }
            set
            {
                _educationViewMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool WorkExperienceViewMoreIsVisible
        {
            get
            {
                return _workExperienceViewMoreIsVisible;
            }
            set
            {
                _workExperienceViewMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool SkillViewMoreIsVisible
        {
            get
            {
                return _skillViewMoreIsVisible;
            }
            set
            {
                _skillViewMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool QualificationViewMoreIsVisible
        {
            get
            {
                return _qualificationViewMoreIsVisible;
            }
            set
            {
                _qualificationViewMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool LicenceViewMoreIsVisible
        {
            get
            {
                return _licenceViewMoreIsVisible;
            }
            set
            {
                _licenceViewMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool DocumentViewMoreIsVisible
        {
            get
            {
                return _documentViewMoreIsVisible;
            }
            set
            {
                _documentViewMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool ReferenceViewMoreIsVisible
        {
            get
            {
                return _referenceViewMoreIsVisible;
            }
            set
            {
                _referenceViewMoreIsVisible = value;
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
        public ICommand EducationViewMoreCommand => new Command(EducationViewMoreCommandAsync);
        public ICommand WorkExperienceViewMoreCommand => new Command(WorkExperienceViewMoreCommandAsync);
        public ICommand SkillViewMoreCommand => new Command(SkillViewMoreCommandAsync);
        public ICommand QualificationViewMoreCommand => new Command(QualificationViewMoreCommandAsync);
        public ICommand LicenceViewMoreCommand => new Command(LicenceViewMoreCommandAsync);
        public ICommand DocumentViewMoreCommand => new Command(DocumentViewMoreCommandAsync);
        public ICommand ReferenceViewMoreCommand => new Command(ReferenceViewMoreCommandAsync);
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

        private void EducationViewMoreCommandAsync()
        {
            GetBindingEducation();
        }

        private void WorkExperienceViewMoreCommandAsync()
        {
            GetBindingWorkExprience();
        }

        private void SkillViewMoreCommandAsync()
        {
            GetBindingSkill();
        }

        private void QualificationViewMoreCommandAsync()
        {
            GetBindingQualification();
        }

        private void LicenceViewMoreCommandAsync()
        {
            GetBindinLicence();
        }

        private void DocumentViewMoreCommandAsync()
        {
            GetBindingDocument();
        }

        private void ReferenceViewMoreCommandAsync()
        {
            GetBindingReference();
        }

        private void ListView_ItemTapped()
        {

        }

        #region Initdata
        public override async Task InitializeAsync(object navigationData)
        {
            var pop = await _dialogService.OpenLoadingPopup();
            var contactID = App.ContactID;
            dynamic obj = await _candidateDetailService.GetEmployerCandidateDetail();
            //Get all list
            educationList = JsonConvert.DeserializeObject<List<ContactEducation>>(obj["Educations"].ToString());
            workExprienceList = JsonConvert.DeserializeObject<List<ContactWorkExprience>>(obj["WorkExperiences"].ToString());
            skillList = JsonConvert.DeserializeObject<List<ContactSkill>>(obj["Skills"].ToString());
            qualificationList = JsonConvert.DeserializeObject<List<ContactQualification>>(obj["Qualifications"].ToString());
            licenceList = JsonConvert.DeserializeObject<List<ContactLicence>>(obj["Licences"].ToString());
            documentList = JsonConvert.DeserializeObject<List<Document>>(obj["Documents"].ToString());
            referenceList = JsonConvert.DeserializeObject<List<ContactReference>>(obj["References"].ToString());
            List<InterestedRole> roles = JsonConvert.DeserializeObject<List<InterestedRole>>(obj["Role"].ToString());
            List<ContactLink>  contactLinks = new List<ContactLink>();
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
                //Educations = educationList,
                //WorkExpriences = workExprienceList,
                //Skills = skillList,
                //Qualifications = qualificationList,
                //Licences = licenceList,
                //Documents = documentList,
                //References = referenceList,
                ContactLinks = contactLinks
            };

            GetBindingDocument();
            GetBindingEducation();
            GetBindingQualification();
            GetBindingReference();
            GetBindingSkill();
            GetBindingWorkExprience();
            GetBindinLicence();

            //ContactLinksListViewHeightRequest = (contactLinks.Count * 28 - 1);
            //EducationListViewHeightRequest = (educationList.Count * 91 - 1 * (educationList.Count > 1 ? 1 : 0));
            //WorkExperienceListViewHeightRequest = (workExprienceList.Count * 91 - 1 * (workExprienceList.Count > 1 ? 1 : 0));
            //SkillListViewHeightRequest = (skillList.Count * 31 - 1 * (skillList.Count > 1 ? 1 : 0));
            //QualificationListViewHeightRequest = (qualificationList.Count * 91 - 1 * (qualificationList.Count > 1 ? 1 : 0));
            //LicenceListViewHeightRequest = (licenceList.Count * 61 - 1 * (licenceList.Count > 1 ? 1 : 0));
            //DocumentListViewHeightRequest = (documentList.Count * 31 - 1 * (documentList.Count > 1 ? 1 : 0));
            //ReferenceListViewHeightRequest = (referenceList.Count * 91 - 1 * (referenceList.Count > 1 ? 1 : 0));
            await _dialogService.CloseLoadingPopup(pop);
        }
        #endregion

        #region Method
        private void GetBindingEducation()
        {
            if (educationList.Count > educationCount + 3)
            {
                educationCount = educationList.Count > educationCount + 3 ? educationCount + 3 : educationList.Count;
                EducationViewMoreText = "View More(" + educationCount.ToString() + ")";
            }
            else
            {
                educationCount = educationList.Count;
                EducationViewMoreIsVisible = false;
            }
            Profile.Educations = educationList.Take(educationCount).ToList();
            Profile = Profile;
            EducationListViewHeightRequest = (educationCount * 91 - 1 * (educationCount > 1 ? 1 : 0));
        }

        private void GetBindingWorkExprience()
        {
            if (workExprienceList.Count > workExprienceCount + 3)
            {
                workExprienceCount = workExprienceList.Count > workExprienceCount + 3 ? workExprienceCount + 3 : workExprienceList.Count;
                WorkExperienceViewMoreText = "View More(" + workExprienceCount.ToString() + ")";
            }
            else
            {
                workExprienceCount = workExprienceList.Count;
                _workExperienceViewMoreIsVisible = false;
            }

            Profile.WorkExpriences = workExprienceList.Take(workExprienceCount).ToList();
            Profile = Profile;
            WorkExperienceListViewHeightRequest = (workExprienceCount * 91 - 1 * (workExprienceCount > 1 ? 1 : 0));
        }

        private void GetBindingSkill()
        {
            if (skillList.Count > skillCount + 3)
            {
                skillCount = skillList.Count > skillCount + 3 ? skillCount + 3 : skillList.Count;
                SkillViewMoreText = "View More(" + skillCount.ToString() + ")";
            }
            else
            {
                skillCount = skillList.Count;
                SkillViewMoreIsVisible = false;
            }

            Profile.Skills = skillList.Take(skillCount).ToList();
            Profile = Profile;
            SkillListViewHeightRequest = (skillCount * 91 - 1 * (skillCount > 1 ? 1 : 0));
        }

        private void GetBindingQualification()
        {
            if (qualificationList.Count > qualificationCount + 3)
            {
                qualificationCount = qualificationList.Count > qualificationCount + 3 ? qualificationCount + 3 : qualificationList.Count;
                QualificationViewMoreText = "View More(" + qualificationCount.ToString() + ")";
            }
            else
            {
                qualificationCount = qualificationList.Count;
                QualificationViewMoreIsVisible = false;
            }

            Profile.Qualifications = qualificationList.Take(qualificationCount).ToList();
            Profile = Profile;
            QualificationListViewHeightRequest = (qualificationCount * 91 - 1 * (qualificationCount > 1 ? 1 : 0));
        }

        private void GetBindinLicence()
        {
            if (licenceList.Count > licenceCount + 3)
            {
                licenceCount = licenceList.Count > licenceCount + 3 ? licenceCount + 3 : licenceList.Count;
                LicenceViewMoreText = "View More(" + licenceCount.ToString() + ")";
            }
            else
            {
                licenceCount = licenceList.Count;
                LicenceViewMoreIsVisible = false;
            }

            Profile.Licences = licenceList.Take(licenceCount).ToList();
            Profile = Profile;
            LicenceListViewHeightRequest = (licenceCount * 61 - 1 * (licenceCount > 1 ? 1 : 0));
        }

        private void GetBindingDocument()
        {
            if (documentList.Count > documentCount + 3)
            {
                documentCount = documentList.Count > documentCount + 3 ? documentCount + 3 : documentList.Count;
                DocumentViewMoreText = "View More(" + documentCount.ToString() + ")";
            }
            else
            {
                documentCount = documentList.Count;
                DocumentViewMoreIsVisible = false;
            }

            Profile.Documents = documentList.Take(documentCount).ToList();
            Profile = Profile;
            DocumentListViewHeightRequest = (documentCount * 91 - 1 * (documentCount > 1 ? 1 : 0));
        }

        private void GetBindingReference()
        {
            if (referenceList.Count > referenceCount + 3)
            {
                referenceCount = referenceList.Count > referenceCount + 3 ? referenceCount + 3 : referenceList.Count;
                ReferenceViewMoreText = "View More(" + referenceCount.ToString() + ")";
            }
            else
            {
                referenceCount = referenceList.Count;
                ReferenceViewMoreIsVisible = false;
            }

            Profile.References = referenceList.Take(referenceCount).ToList();
            Profile = Profile;
            ReferenceListViewHeightRequest = (referenceCount * 91 - 1 * (referenceCount > 1 ? 1 : 0));
        }

        #endregion
    }
}