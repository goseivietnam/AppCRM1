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
using Syncfusion.ListView.XForms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

        private Contact _profile;

        private List<SfListView> ListViews { get; set; } = new List<SfListView>();

        private CollapsableList<ContactEducation> _educationList;
        private CollapsableList<ContactWorkExprience> _workExprienceList;
        private CollapsableList<ContactSkill> _skillList;
        private CollapsableList<ContactQualification> _qualificationList;
        private CollapsableList<ContactLicence> _licenseList;
        private CollapsableList<Document> _documentList;
        private CollapsableList<ContactReference> _referenceList;

        public CandidateProfileViewModel(ICandidateDetailsService candidateDetailService, INavigationService navigationService, IDialogService dialogService)
        {
            _candidateDetailService = candidateDetailService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public Contact Profile
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
        
        public CollapsableList<ContactEducation> EducationList
        {
            get
            {
                return _educationList;
            }
            set
            {
                _educationList = value;
                OnPropertyChanged();
            }
        }
        public CollapsableList<ContactWorkExprience> WorkExperienceList
        {
            get
            {
                return _workExprienceList;
            }
            set
            {
                _workExprienceList = value;
                OnPropertyChanged();
            }
        }
        public CollapsableList<ContactSkill> SkillList
        {
            get
            {
                return _skillList;
            }
            set
            {
                _skillList = value;
                OnPropertyChanged();
            }
        }
        public CollapsableList<ContactQualification> QualificationList
        {
            get
            {
                return _qualificationList;
            }
            set
            {
                _qualificationList = value;
                OnPropertyChanged();
            }
        }
        public CollapsableList<ContactLicence> LicenseList
        {
            get
            {
                return _licenseList;
            }
            set
            {
                _licenseList = value;
                OnPropertyChanged();
            }
        }
        public CollapsableList<Document> DocumentList
        {
            get
            {
                return _documentList;
            }
            set
            {
                _documentList = value;
                OnPropertyChanged();
            }
        }
        public CollapsableList<ContactReference> ReferenceList
        {
            get
            {
                return _referenceList;
            }
            set
            {
                _referenceList = value;
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
        public ICommand ListViewLoadedCommand => new Command((obj) =>
        {
            this.ListViews.Add(obj as SfListView);
        });
        public ICommand EducationViewMoreCommand => new Command((obj) =>
        {
            EducationList.Expand();
            RefreshHeightRequest(obj);
        });
        public ICommand WorkExperienceViewMoreCommand => new Command((obj) =>
        {
            WorkExperienceList.Expand();
            RefreshHeightRequest(obj);
        });
        public ICommand SkillViewMoreCommand => new Command((obj) =>
        {
            SkillList.Expand();
            RefreshHeightRequest(obj);
        });
        public ICommand QualificationViewMoreCommand => new Command((obj) =>
        {
            QualificationList.Expand();
            RefreshHeightRequest(obj);
        });
        public ICommand LicenseViewMoreCommand => new Command((obj) =>
        {
            LicenseList.Expand();
            RefreshHeightRequest(obj);
        });
        public ICommand DocumentViewMoreCommand => new Command((obj) =>
        {
            DocumentList.Expand();
            RefreshHeightRequest(obj);
        });
        public ICommand ReferenceViewMoreCommand => new Command((obj) =>
        {
            ReferenceList.Expand();
            RefreshHeightRequest(obj);
        });

        private void RefreshHeightRequest(object sender)
        {
            var listView = sender as SfListView;
            //VisualContainer visualContainer = listView.GetType().GetRuntimeProperties().First(p => p.Name == "VisualContainer").GetValue(listView) as VisualContainer;
            //var totalextent = (double)visualContainer.GetType().GetRuntimeProperties().FirstOrDefault(container => container.Name == "TotalExtent").GetValue(visualContainer);
            listView.HeightRequest = listView.ItemSize * listView.DataSource.Items.Count() + 50;
        }

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

        #region Initdata
        public override async Task InitializeAsync(object navigationData)
        {
            var pop = await _dialogService.OpenLoadingPopup();
            var contactID = App.ContactID;
            dynamic obj = await _candidateDetailService.GetEmployerCandidateDetail();
            //Get all list
            EducationList = new CollapsableList<ContactEducation>(JsonConvert.DeserializeObject<List<ContactEducation>>(obj["Educations"].ToString()), 3);
            WorkExperienceList = new CollapsableList<ContactWorkExprience>(JsonConvert.DeserializeObject<List<ContactWorkExprience>>(obj["WorkExperiences"].ToString()), 3);
            SkillList = new CollapsableList<ContactSkill>(JsonConvert.DeserializeObject<List<ContactSkill>>(obj["Skills"].ToString()), 3);
            QualificationList = new CollapsableList<ContactQualification>(JsonConvert.DeserializeObject<List<ContactQualification>>(obj["Qualifications"].ToString()), 3);
            LicenseList = new CollapsableList<ContactLicence>(JsonConvert.DeserializeObject<List<ContactLicence>>(obj["Licences"].ToString()), 3);
            DocumentList = new CollapsableList<Document>(JsonConvert.DeserializeObject<List<Document>>(obj["Documents"].ToString()), 3);
            ReferenceList = new CollapsableList<ContactReference>(JsonConvert.DeserializeObject<List<ContactReference>>(obj["References"].ToString()), 3);

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

            Profile = new Contact()
            {
                UserID = contactID,
                AvatarUrl = RequestService.HOST_NAME + "api/Document/GetContactImage?id=" + obj["CandidateDetails"]["ProfileImage"],
                CoverUrl = RequestService.HOST_NAME + "api/Document/GetContactImage?id=" + obj["CandidateDetails"]["CoverImage"],
                FullName = obj["CandidateDetails"]["FullName"],
                RoleAndAddress = interestedRoles + " - " + obj["CandidateDetails"]["CityName"],
                Greeting = "Hi, I am " + obj["CandidateDetails"]["FullName"],
                Introduction = Utilities.HtmlToPlainText(obj["CandidateDetails"]["AboutMe"].ToString()),
                ContactLinks = contactLinks
            };

            CandidateMainViewModel.Current.IsProfilePageRendered = true;
            await _dialogService.CloseLoadingPopup(pop);
            foreach (var listView in this.ListViews)
            {
                this.RefreshHeightRequest(listView);
            }
        }
        #endregion
    }

    public class CollapsableList<T> : INotifyPropertyChanged
    {
        public ObservableCollection<T> Items { get; set; } = new ObservableCollection<T>();
        public IEnumerable<T> AllItems { get; set; }
        public int CollapsedSize { get; set; }
        private CollapseMode CurrentMode { get; set; } = CollapseMode.Init;

        public CollapsableList(IEnumerable<T> collection, int collapsedSize = 10)
        {
            this.AllItems = collection ?? new List<T>();
            this.CollapsedSize = collapsedSize;
            if (this.AllItems.Count() <= this.CollapsedSize)
            {
                this.Expand();
            }
            else
            {
                this.Collapse();
            }
        }

        public bool IsCollapsed { get { return this.CurrentMode == CollapseMode.Collapsed; } }
        public int RemainCount { get { return this.AllItems.Count() - this.Items.Count; } }

        public void Expand()
        {
            if (this.CurrentMode == CollapseMode.Init || this.CurrentMode == CollapseMode.Collapsed)
            {
                this.Items.Clear();
                foreach (T item in this.AllItems)
                {
                    this.Items.Add(item);
                }
                this.CurrentMode = CollapseMode.Expanded;
                OnPropertyChanged("IsCollapsed");
            }
        }

        public void Collapse()
        {
            if (this.CurrentMode == CollapseMode.Init || this.CurrentMode == CollapseMode.Expanded)
            {
                this.Items.Clear();
                foreach (T item in this.AllItems.Take(this.CollapsedSize))
                {
                    this.Items.Add(item);
                }
                this.CurrentMode = CollapseMode.Collapsed;
                OnPropertyChanged("IsCollapsed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private enum CollapseMode
        {
            Init,
            Collapsed,
            Expanded
        }
    }
}