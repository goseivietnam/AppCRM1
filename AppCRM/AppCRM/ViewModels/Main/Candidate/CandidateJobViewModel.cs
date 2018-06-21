using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Navigation;
using AppCRM.ViewModels.Base;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateJobViewModel : ViewModelBase
    {
        private readonly ICandidateJobService _candidateJobService;
        private readonly INavigationService _navigationService;
        private CandidateJob _job;

        // height listview 
        private int _appliedJobListViewHeightRequest;
        private int _referenceCheckJobListViewHeightRequest;
        private int _assessmentJobListViewHeightRequest;
        private int _shortlistJobListViewHeightRequest;


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

        public int AppliedJobListViewHeightRequest
        {
            get
            {
                return _appliedJobListViewHeightRequest;
            }
            set
            {
                _appliedJobListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int ReferenceCheckJobListViewHeightRequest
        {
            get
            {
                return _referenceCheckJobListViewHeightRequest;
            }
            set
            {
                _referenceCheckJobListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int AssessmentJobListViewHeightRequest
        {
            get
            {
                return _assessmentJobListViewHeightRequest;
            }
            set
            {
                _assessmentJobListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int ShortlistJobListViewHeightRequest
        {
            get
            {
                return _shortlistJobListViewHeightRequest;
            }
            set
            {
                _shortlistJobListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }

        public ICommand MasterPageBtnCommand => new Command(MasterPageBtnAsync);
        public ICommand ListViewCommand => new Command(ListView_ItemTapped);

        private void MasterPageBtnAsync()
        {
            (Application.Current.MainPage as MasterDetailPage).IsPresented = true;
        }
        private void ListView_ItemTapped()
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            dynamic obj = await _candidateJobService.GetCandidateJobApplied();
            List<ContactVacancy> contactVacancyList = JsonConvert.DeserializeObject<List<ContactVacancy>>(obj["candidateJobsApplied"].ToString());
            List<string> statusNameList = new List<string>();

            foreach (var item in contactVacancyList)
            {
                if (statusNameList.IndexOf(item.StatusName) > 0)
                {
                    statusNameList.Add(item.StatusName);
                }
            }

            //Job = new CandidateJob
            //{
            //    AppliedJobs = new List<ContactVacancy>
            //    {
            //        new ContactVacancy { JobDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", JobName = "Mechanical Design", CompanyName = "DBS Bank", IsFavorite = true, JobType = "Temporary", Salary = 80205, Location = "Townsville", DayRemain = 15, Status = JobStatus.APPLIED },
            //        new ContactVacancy { JobDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", JobName = "Case Manager", CompanyName = "NCS", IsFavorite = false, JobType = "Apprenticeship", Salary = 92509, Location = "Indiana", DayRemain = 31, Status = JobStatus.APPLIED },
            //        new ContactVacancy { JobDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", JobName = "Social Media Coordinator", CompanyName = "Barclays", IsFavorite = false, JobType = "Casual", Salary = 70247, Location = "Virginia", DayRemain = 22, Status = JobStatus.APPLIED },
            //        new ContactVacancy { JobDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", JobName = "Project Manager", CompanyName = "Nanyang Technological University", IsFavorite = false, JobType = "Subcontract", Salary = 68513, Location = "Pennsylvania", DayRemain = 5, Status = JobStatus.APPLIED }
            //    },
            //    ReferenceCheckJobs = new List<ContactVacancy>
            //    {
            //        new ContactVacancy { JobDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", JobName = "Case Manager", CompanyName = "DBS", IsFavorite = true, JobType = "Subcontract", Salary = 98277, Location = "South Carolina", DayRemain = 3, Status = JobStatus.REFERENCE_CHECK }
            //    },
            //    AssessmentJobs = new List<ContactVacancy>
            //    {
            //        new ContactVacancy { JobDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", JobName = "System Engineer", CompanyName = "Citibank", IsFavorite = false, JobType = "Subcontract", Salary = 3729, Location = "Hawaii", DayRemain = 16, Status = JobStatus.ASSESSMENT }
            //    },
            //    ShortlistJobs = new List<ContactVacancy>
            //    {
            //        new ContactVacancy { JobDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", JobName = "Manager", CompanyName = "Accenture", IsFavorite = false, JobType = "Subcontract", Salary = 60119, Location = "New Hampshire", DayRemain = 10, Status = JobStatus.SHORTLIST },
            //        new ContactVacancy { JobDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", JobName = "Commercial Sales Executive", CompanyName = "DBS Bank", IsFavorite = false, JobType = "Apprenticeship", Salary = 47396, Location = "Wisconsin", DayRemain = 19, Status = JobStatus.SHORTLIST }
            //    },
            //    NeedActionAssessments = new List<AssessmentDetail>
            //    {
            //        new AssessmentDetail { AssessmentDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", AssessmentName = "Assessment", JobName = "Job", CompanyName = "Company", Location = "City", Status = AssessmentStatus.NEED_ACTION }
            //    },
            //    CompleteAssessments = new List<AssessmentDetail>
            //    {
            //        new AssessmentDetail { AssessmentDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", AssessmentName = "Assessment", JobName = "Job", CompanyName = "Company", Location = "City", Status = AssessmentStatus.COMPLETE },
            //        new AssessmentDetail { AssessmentDetailId = Guid.NewGuid().ToString(), ImageSource = "https://i.imgur.com/fSZz5Ta.png", AssessmentName = "Assessment", JobName = "Job", CompanyName = "Company", Location = "City", Status = AssessmentStatus.COMPLETE }
            //    }
            //};

            AppliedJobListViewHeightRequest = Job.AppliedJobs.Count * 120 + 39;
            ReferenceCheckJobListViewHeightRequest = Job.ReferenceCheckJobs.Count * 120 + 39;
            AssessmentJobListViewHeightRequest = Job.AssessmentJobs.Count * 120 + 39;
            ShortlistJobListViewHeightRequest = Job.ShortlistJobs.Count * 120 + 39;
            IsBusy = false;
        }
    }
}
