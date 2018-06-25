using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Navigation;
using AppCRM.Tools;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Newtonsoft.Json;
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
            DateTime opendate;
            try
            {
                opendate = DateTime.Parse(obj["jobDetail"]["OpenDate"].ToString());
            }
            catch
            {
                opendate = DateTime.MinValue;
            }
            TimeSpan opendateInt = DateTime.Now - opendate;

            if (obj["jobDetail"] != null)
            {
                string Title = obj["jobDetail"]["Title"];
                string worksiteName = obj["jobDetail"]["WorksiteName"];
                bool isPromoted = obj["jobDetail"]["IsPromoted"];
                string jobType = obj["jobDetail"]["JobType"];
                string salary = obj["jobDetail"]["Salary"];
                string status = obj["jobDetail"]["Status"];
                string description = "";
                if (obj["jobDetail"]["Description"] != null)
                {
                    description = Utilities.HtmlToPlainText(obj["jobDetail"]["Description"].ToString());
                }
                Models.Account account = new Models.Account();
                if (obj["jobDetail"]["Account"] != null)
                {
                    account = JsonConvert.DeserializeObject<Models.Account>(obj["jobDetail"]["Account"].ToString());
                }
                List<JobRequire> requires = new List<JobRequire>();
                if (obj["requiredList"] != null)
                {
                    requires = JsonConvert.DeserializeObject<List<JobRequire>>(obj["requiredList"]).ToString();
                }
                Job = new Vacancy
                {
                    VacancyID = id,
                    //ImageSource = "https://i.imgur.com/fSZz5Ta.png",
                    Title = Title,
                    WorksiteName = worksiteName,
                    IsPromoted = isPromoted,
                    JobType = jobType,
                    Salary = salary,
                    Status = status,
                    OpenDate = opendate,
                    OpenDateInt = (int)opendateInt.TotalDays,
                    Description = description,
                    Account = account,
                    Requires = requires,
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
