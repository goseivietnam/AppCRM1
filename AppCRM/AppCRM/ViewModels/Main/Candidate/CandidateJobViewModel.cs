using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate.Job;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateJobViewModel : ViewModelBase
    {
        private readonly ICandidateJobService _candidateJobService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private List<ContactVacancyGroup> _jobGroups;

        private List<ContactTemplate> _needDoAssessemnt;
        private List<ContactTemplate> _completeAssessemnt;

        private List<ContactTemplate> _needDoAssessemntList = new List<ContactTemplate>();
        private List<ContactTemplate> _completeAssessemntList = new List<ContactTemplate>();
        private List<ContactVacancy> _contactVacanciesList = new List<ContactVacancy>();

        // height listview
        private int _jobListViewHeightRequest;
        private int _needActionListViewHeightRequest;
        private int _completetListViewHeightRequest;

        private string _jobSearchedText;
        private string _assessmentSearchedText;

        private bool _jobNoFoundIsVisible;
        private bool _assessmentNoFoundIsVisible;

        public CandidateJobViewModel(ICandidateJobService candidateJobService, INavigationService navigationService, IDialogService dialogService)
        {
            _candidateJobService = candidateJobService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public List<ContactTemplate> NeedDoAssessement
        {
            get
            {
                return _needDoAssessemnt;
            }
            set
            {
                _needDoAssessemnt = value;
                OnPropertyChanged();
            }
        }

        public List<ContactTemplate> CompleteAssessement
        {
            get
            {
                return _completeAssessemnt;
            }
            set
            {
                _completeAssessemnt = value;
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

        public string JobSearchedText
        {
            get
            {
                return _jobSearchedText;
            }
            set
            {
                _jobSearchedText = value;
                JobSearchCommandExecute(_jobSearchedText);
                OnPropertyChanged();
            }
        }
        public string AssessmentSearchedText
        {
            get
            {
                return _assessmentSearchedText;
            }
            set
            {
                _assessmentSearchedText = value;
                AssessmentSearchCommandExecute(_assessmentSearchedText);
                OnPropertyChanged();
            }
        }

        public bool JobNoFoundIsVisible
        {
            get
            {
                return _jobNoFoundIsVisible;
            }
            set
            {
                _jobNoFoundIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool AssessmentNoFoundIsVisible
        {
            get
            {
                return _assessmentNoFoundIsVisible;
            }
            set
            {
                _assessmentNoFoundIsVisible = value;
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
            var pop = await _dialogService.OpenLoadingPopup();
            JobNoFoundIsVisible = false;
            AssessmentNoFoundIsVisible = false;

            JObject objcontactVacancies = await _candidateJobService.GetCandidateJobApplied();

            ContactTemplateFilter filter = new ContactTemplateFilter
            {
                CurrentPage = 1,
                PageSize = 100,
                IsOpen = true
            };
            Dictionary<string, object> objAssessments = await _candidateJobService.GetAssessment(filter);

            if (objcontactVacancies["candidateJobsApplied"] != null)
            {
                _contactVacanciesList = JsonConvert.DeserializeObject<List<ContactVacancy>>(objcontactVacancies["candidateJobsApplied"].ToString());
            }


            CompleteAssessement = new List<ContactTemplate>();
            NeedDoAssessement = new List<ContactTemplate>();

            List<ContactTemplate> complete = new List<ContactTemplate>();
            List<ContactTemplate> needdo = new List<ContactTemplate>();
            if (objAssessments["templates"] != null)
            {
                List<ContactTemplate> Assessments = JsonConvert.DeserializeObject<List<ContactTemplate>>(objAssessments["templates"].ToString());
                foreach (var assessment in Assessments)
                {
                    if (assessment.IsCompleted)
                    {
                        complete.Add(assessment);
                    }
                    else
                    {
                        needdo.Add(assessment);
                    }
                }
            }

            _completeAssessemntList.AddRange(complete);
            _needDoAssessemntList.AddRange(needdo);

            CompleteAssessement = complete;
            NeedDoAssessement = needdo;

            //Populate JobGroup
            List<ContactVacancyGroup> groups = new List<ContactVacancyGroup>();
            foreach (var vacancy in _contactVacanciesList)
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

            JobListViewHeightRequest = _contactVacanciesList.Count * 120 + JobGroups.Count * 38;
            NeedActionListViewHeightRequest = _needDoAssessemntList.Count * 90 + 38;
            CompleteListViewHeightRequest = _completeAssessemntList.Count * 90 + 38;

            CandidateMainViewModel.Current.IsJobPageRendered = true;
            await _dialogService.CloseLoadingPopup(pop);
        }

        private void JobSearchCommandExecute(string _search)
        {
            JobGroups.RemoveRange(0, JobGroups.Count);

            List<ContactVacancy> ContactVacancies = new List<ContactVacancy>(_contactVacanciesList.Where(C => C.PoisitionName.ToLowerInvariant().Contains(_search.ToLowerInvariant())));

            if (ContactVacancies.Count == 0)
            {
                JobNoFoundIsVisible = true;
            }
            else
            {
                JobNoFoundIsVisible = false;
            }

            //Populate JobGroup
            List<ContactVacancyGroup> groups = new List<ContactVacancyGroup>();
            foreach (var vacancy in ContactVacancies)
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

            JobListViewHeightRequest = ContactVacancies.Count * 120 + JobGroups.Count * 38;
        }

        private void AssessmentSearchCommandExecute(string _search)
        {
            NeedDoAssessement.RemoveRange(0, NeedDoAssessement.Count);
            CompleteAssessement.RemoveRange(0, CompleteAssessement.Count);

            List<ContactTemplate> needDoTemplates = new List<ContactTemplate>();
            foreach (var item in _needDoAssessemntList)
            {
                if (item.TemplateName.ToLowerInvariant().Contains(_search.ToLowerInvariant()))
                {
                    needDoTemplates.Add(item);
                }
            }
            NeedDoAssessement = needDoTemplates;

            List<ContactTemplate> completeTemplates = new List<ContactTemplate>();
            foreach (var item in _completeAssessemntList)
            {
                if (item.TemplateName.ToLowerInvariant().Contains(_search.ToLowerInvariant()))
                {
                    completeTemplates.Add(item);
                }
            }
            CompleteAssessement = completeTemplates;

            if (CompleteAssessement.Count == 0 && NeedDoAssessement.Count == 0)
            {
                AssessmentNoFoundIsVisible = true;
            }
            else
            {
                AssessmentNoFoundIsVisible = false;
            }

            NeedActionListViewHeightRequest = NeedDoAssessement.Count * 90 + 38;
            CompleteListViewHeightRequest = CompleteAssessement.Count * 90 + 38;
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
}
