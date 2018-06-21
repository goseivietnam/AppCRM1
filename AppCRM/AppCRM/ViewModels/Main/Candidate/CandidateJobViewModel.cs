using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate.Job;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateJobViewModel : ViewModelBase
    {
        private readonly ICandidateJobService _candidateJobService;
        private readonly INavigationService _navigationService;
        private CandidateJob _job;
        private List<ContactVacancyGroup> _jobGroups;

        // height listview
        private int _jobListViewHeightRequest;
        private int _needActionListViewHeightRequest;
        private int _completetListViewHeightRequest;


        public CandidateJobViewModel(ICandidateJobService candidateJobService, INavigationService navigationService)
        {
            _candidateJobService = candidateJobService;
            _navigationService = navigationService;
        }

        public CandidateJob Job
        {
            get
            {
                return _job;
            }
            set
            {
                _job = value;
                OnPropertyChanged();
            }
        }
        public List<ContactVacancyGroup> JobGroups
        {
            get
            {
                return _jobGroups;
            }
            set
            {
                _jobGroups = value;
                OnPropertyChanged();
            }
        }

        public int JobListViewHeightRequest
        {
            get
            {
                return _jobListViewHeightRequest;
            }
            set
            {
                _jobListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int NeedActionListViewHeightRequest
        {
            get
            {
                return _needActionListViewHeightRequest;
            }
            set
            {
                _needActionListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int CompleteListViewHeightRequest
        {
            get
            {
                return _completetListViewHeightRequest;
            }
            set
            {
                _completetListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }

        public ICommand MasterPageBtnCommand => new Command(MasterPageBtnAsync);
        public ICommand JobListViewCommand => new AsyncCommand(JobListView_ItemTappedAsync);
        public ICommand AssessmentListViewCommand => new Command(AssessmentListView_ItemTapped);

        private void MasterPageBtnAsync()
        {
            (Application.Current.MainPage as MasterDetailPage).IsPresented = true;
        }
        private async Task JobListView_ItemTappedAsync(object jobDetail)
        {
            if (jobDetail != null)
            {
                await _navigationService.NavigateToPopupAsync<JobDetailViewModel>((jobDetail as ContactVacancy).ContactVacancyID, true);
            }
        }
        private void AssessmentListView_ItemTapped(object assessmentDetail)
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            Job = new CandidateJob
            {
                ContactVacancies = new List<ContactVacancy>
                {
                    new ContactVacancy { ContactVacancyID = Guid.NewGuid(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", PoisitionName = "Mechanical Design", WorksiteName = "DBS Bank", IsPromoted = true, JobTypeName = "Temporary", MaxSalary = 80205, Location = "Townsville", AppliedDate = (DateTime.Now.AddDays(-15)), StatusName = JobStatus.APPLIED },
                    new ContactVacancy { ContactVacancyID = Guid.NewGuid(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", PoisitionName = "Case Manager", WorksiteName = "NCS", IsPromoted = false, JobTypeName = "Apprenticeship", MaxSalary = 92509, Location = "Indiana", AppliedDate = (DateTime.Now.AddDays(-31)), StatusName = JobStatus.APPLIED },
                    new ContactVacancy { ContactVacancyID = Guid.NewGuid(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", PoisitionName = "Social Media Coordinator", WorksiteName = "Barclays", IsPromoted = false, JobTypeName = "Casual", MaxSalary = 70247, Location = "Virginia", AppliedDate = (DateTime.Now.AddDays(-22)), StatusName = JobStatus.APPLIED },
                    new ContactVacancy { ContactVacancyID = Guid.NewGuid(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", PoisitionName = "Project Manager", WorksiteName = "Nanyang Technological University", IsPromoted = false, JobTypeName = "Subcontract", MaxSalary = 68513, Location = "Pennsylvania", AppliedDate = (DateTime.Now.AddDays(-5)), StatusName = JobStatus.APPLIED },
                    new ContactVacancy { ContactVacancyID = Guid.NewGuid(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", PoisitionName = "Case Manager", WorksiteName = "DBS", IsPromoted = true, JobTypeName = "Subcontract", MaxSalary = 98277, Location = "South Carolina", AppliedDate = (DateTime.Now.AddDays(-3)), StatusName = JobStatus.REFERENCE_CHECK },
                    new ContactVacancy { ContactVacancyID = Guid.NewGuid(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", PoisitionName = "System Engineer", WorksiteName = "Citibank", IsPromoted = false, JobTypeName = "Subcontract", MaxSalary = 3729, Location = "Hawaii", AppliedDate = (DateTime.Now.AddDays(-16)), StatusName = JobStatus.ASSESSMENT },
                    new ContactVacancy { ContactVacancyID = Guid.NewGuid(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", PoisitionName = "Manager", WorksiteName = "Accenture", IsPromoted = false, JobTypeName = "Subcontract", MaxSalary = 60119, Location = "New Hampshire", AppliedDate = (DateTime.Now.AddDays(-10)), StatusName = JobStatus.SHORTLIST },
                    new ContactVacancy { ContactVacancyID = Guid.NewGuid(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", PoisitionName = "Commercial Sales Executive", WorksiteName = "DBS Bank", IsPromoted = false, JobTypeName = "Apprenticeship", MaxSalary = 47396, Location = "Wisconsin", AppliedDate = (DateTime.Now.AddDays(-19)), StatusName = JobStatus.SHORTLIST }
                },
                NeedActionAssessments = new List<AssessmentDetail>
                {
                    new AssessmentDetail { AssessmentDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", AssessmentName = "Assessment", JobName = "Job", CompanyName = "Company", Location = "City", Status = AssessmentStatus.NEED_ACTION }
                },
                CompleteAssessments = new List<AssessmentDetail>
                {
                    new AssessmentDetail { AssessmentDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", AssessmentName = "Assessment", JobName = "Job", CompanyName = "Company", Location = "City", Status = AssessmentStatus.COMPLETE },
                    new AssessmentDetail { AssessmentDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", AssessmentName = "Assessment", JobName = "Job", CompanyName = "Company", Location = "City", Status = AssessmentStatus.COMPLETE }
                }
            };

            //Populate JobGroup
            List<ContactVacancyGroup> groups = new List<ContactVacancyGroup>();
            foreach (var vacancy in Job.ContactVacancies)
            {
                var statusName = vacancy.StatusName;
                if (groups.Any(r => r.StatusName == statusName))
                {
                    groups.Single(r => r.StatusName == statusName).Add(vacancy);
                }
                else
                {
                    groups.Add(new ContactVacancyGroup(statusName) { vacancy });
                }
            }
            JobGroups = groups;

            JobListViewHeightRequest = Job.ContactVacancies.Count * 120 + JobGroups.Count * 38;
            NeedActionListViewHeightRequest = Job.NeedActionAssessments.Count * 90 + 38;
            CompleteListViewHeightRequest = Job.CompleteAssessments.Count * 90 + 38;

            IsBusy = false;
        }
    }

    public class ContactVacancyGroup : List<ContactVacancy>
    {
        public string StatusName { get; set; }
        public string DisplayHeader { get { return string.Format("{0} ({1})", this.StatusName.ToUpper(), base.Count); } }

        public ContactVacancyGroup(string statusName)
        {
            StatusName = statusName;
        }
    }
}
