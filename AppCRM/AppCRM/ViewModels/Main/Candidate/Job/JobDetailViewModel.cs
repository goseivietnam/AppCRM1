using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Job
{
    public class JobDetailViewModel : ViewModelBase
    {
        private readonly ICandidateJobService _candidateJobService;
        private readonly INavigationService _navigationService;
        private Vacancy _job;

        private int _todoTaskListViewHeightRequest;
        private int _completeTaskListViewHeightRequest;
        private int _attachmentListViewHeightRequest;

        public JobDetailViewModel(ICandidateJobService candidateJobService, INavigationService navigationService)
        {
            _candidateJobService = candidateJobService;
            _navigationService = navigationService;
        }

        public Vacancy Job
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

        public int TodoTaskListViewHeightRequest
        {
            get
            {
                return _todoTaskListViewHeightRequest;
            }
            set
            {
                _todoTaskListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int CompleteTaskListViewHeightRequest
        {
            get
            {
                return _completeTaskListViewHeightRequest;
            }
            set
            {
                _completeTaskListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int AttachmentListViewHeightRequest
        {
            get
            {
                return _attachmentListViewHeightRequest;
            }
            set
            {
                _attachmentListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand ListViewCommand => new Command(ListViewTapped);

        private async Task BtnBackAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        private void ListViewTapped()
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            var id = (Guid)navigationData;
            IsBusy = true;
            dynamic obj = await _candidateJobService.GetVacancyDetails(id);

            if (obj["jobDetail"] != null)
            {
                Job = new Vacancy
                {
                    VacancyID = id,
                    //ImageSource = "https://i.imgur.com/fSZz5Ta.png",
                    Title = obj["jobDetail"]["Position"](0).Name,
                    WorksiteName = obj["jobDetail"]["WorksiteName"],
                    IsPromoted = obj["jobDetail"]["IsPromoted"],
                    JobType = obj["jobDetail"]["JobType"],
                    Salary = obj["jobDetail"]["Salary"],
                    Status = obj["jobDetail"]["Status"],
                    OpenDate = obj["jobDetail"]["OpenDate"],
                    Description = obj["jobDetail"]["Description"]
                    //    Requires = new List<JobRequire>
                    //{
                    //    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Adaptability" },
                    //    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Attention To Detail" },
                    //    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Awareness" },
                    //    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Collaboration" },
                    //    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Composure Under Pressure" },
                    //    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Creativity & Innovation" }
                    //},
                    //    ToDoTasks = new List<JobTask>
                    //{
                    //    new JobTask { JobTaskId = Guid.NewGuid().ToString(), TaskName = "Prepare for implementation", CreatedBy = "Hedge Fund Principal", IsComplete = false }
                    //},
                    //    CompleteTasks = new List<JobTask>
                    //{
                    //    new JobTask { JobTaskId = Guid.NewGuid().ToString(), TaskName = "Deﬁne users and workﬂow", CreatedBy = "Compensation Analyst", IsComplete = true },
                    //    new JobTask { JobTaskId = Guid.NewGuid().ToString(), TaskName = "Deﬁne the server conﬁguration.", CreatedBy = "Investment Advisor", IsComplete = true },
                    //    new JobTask { JobTaskId = Guid.NewGuid().ToString(), TaskName = "Conﬁgure ﬁltering, if appropriate.", CreatedBy = "Actuary", IsComplete = true }
                    //},
                    //    Attachments = new List<JobAttachment>
                    //{
                    //    new JobAttachment { JobAttachmentId = Guid.NewGuid().ToString(), AttachmentName = "Prepare for implementation", CreatedBy = "Hedge Fund Principal" },
                    //    new JobAttachment { JobAttachmentId = Guid.NewGuid().ToString(), AttachmentName = "Deﬁne users and workﬂow", CreatedBy = "Compensation Analyst" },
                    //    new JobAttachment { JobAttachmentId = Guid.NewGuid().ToString(), AttachmentName = "Deﬁne the server conﬁguration.", CreatedBy = "Investment Advisor" },
                    //    new JobAttachment { JobAttachmentId = Guid.NewGuid().ToString(), AttachmentName = "Conﬁgure ﬁltering, if appropriate.", CreatedBy = "Actuary" }
                    //}
                };

                //TodoTaskListViewHeightRequest = Job.ToDoTasks.Count * 60 + 38;
                //CompleteTaskListViewHeightRequest = Job.CompleteTasks.Count * 60 + 40;
                //AttachmentListViewHeightRequest = Job.Attachments.Count * 60 + 38;
            }
            IsBusy = false;
        }
    }
}
