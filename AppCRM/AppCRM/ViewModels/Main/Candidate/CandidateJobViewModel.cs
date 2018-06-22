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
using Newtonsoft.Json;
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
                await _navigationService.NavigateToPopupAsync<JobDetailViewModel>((jobDetail as ContactVacancy).VacancyID, true);
            }
        }

        private void AssessmentListView_ItemTapped(object assessmentDetail)
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            dynamic objcontactVacancies = await _candidateJobService.GetCandidateJobApplied();

            ContactTemplateFilter filter = new ContactTemplateFilter
            {
                CurrentPage = 1,
                PageSize = 100,
                IsOpen = true
            };
            dynamic objAssessments = await _candidateJobService.GetAssessment(filter);

            Job = new CandidateJob();
            Job.ContactVacancies = new List<ContactVacancy>();
            Job.NeedActionAssessments = new List<ContactTemplate>();
            Job.CompleteAssessments = new List<ContactTemplate>();

            if (objcontactVacancies["candidateJobsApplied"] != null)
            {
                Job.ContactVacancies = JsonConvert.DeserializeObject<List<ContactVacancy>>(objcontactVacancies["candidateJobsApplied"].ToString());
            }
            if (objAssessments["templates"] != null)
            {
                List<ContactTemplate> Assessments = JsonConvert.DeserializeObject<List<ContactTemplate>>(objAssessments["templates"].ToString());
                foreach (var assessment in Assessments)
                {
                    if (assessment.IsCompleted)
                    {
                       Job.CompleteAssessments.Add(assessment);
                    }
                    else
                    {
                        Job.NeedActionAssessments.Add(assessment);
                    }
                }
            }

            //Populate JobGroup
            List<ContactVacancyGroup> groups = new List<ContactVacancyGroup>();
            foreach (var vacancy in Job.ContactVacancies)
            {
                //vacancy.ImageSource = 
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
