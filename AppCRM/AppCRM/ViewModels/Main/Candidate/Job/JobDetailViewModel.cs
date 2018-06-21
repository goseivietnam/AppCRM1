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
        private JobDetail _job;

        private int _todoTaskListViewHeightRequest;
        private int _completeTaskListViewHeightRequest;
        private int _attachmentListViewHeightRequest;

        public JobDetailViewModel(ICandidateJobService candidateJobService, INavigationService navigationService)
        {
            _candidateJobService = candidateJobService;
            _navigationService = navigationService;
        }

        public JobDetail Job
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
            var id = (string)navigationData;
            Job = new JobDetail
            {
                JobDetailId = id,
                ImageSource = "https://i.imgur.com/fSZz5Ta.png",
                JobName = "Mechanical Design",
                CompanyName = "DBS Bank",
                IsFavorite = true,
                JobType = "Temporary",
                Salary = 80205,
                Location = "Townsville",
                DayRemain = 15,
                Status = JobStatus.APPLIED,
                Description = "The Power Delivery Services Business Group is seeking an Electrical Designer with physical substation design experience to join their team in its Chattanooga, Tennessee ofﬁce. This position will allow you the opportunity to work with some of the best in the power industry and with a ﬁrm that has been an industry leader over the past 125 years!\n\nThis position will offer you the opportunity to utilize and expand your drafting and design skills, while working in a multi-disciplined team environment with other designers and/or engineers in the preparation of design drawings based on design input.",
                Requires = new List<JobRequire>
                {
                    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Adaptability" },
                    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Attention To Detail" },
                    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Awareness" },
                    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Collaboration" },
                    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Composure Under Pressure" },
                    new JobRequire { JobRequireId = Guid.NewGuid().ToString(), RequireName = "Creativity & Innovation" }
                },
                ToDoTasks = new List<JobTask>
                {
                    new JobTask { JobTaskId = Guid.NewGuid().ToString(), TaskName = "Prepare for implementation", CreatedBy = "Hedge Fund Principal", IsComplete = false }
                },
                CompleteTasks = new List<JobTask>
                {
                    new JobTask { JobTaskId = Guid.NewGuid().ToString(), TaskName = "Deﬁne users and workﬂow", CreatedBy = "Compensation Analyst", IsComplete = true },
                    new JobTask { JobTaskId = Guid.NewGuid().ToString(), TaskName = "Deﬁne the server conﬁguration.", CreatedBy = "Investment Advisor", IsComplete = true },
                    new JobTask { JobTaskId = Guid.NewGuid().ToString(), TaskName = "Conﬁgure ﬁltering, if appropriate.", CreatedBy = "Actuary", IsComplete = true }
                },
                Attachments = new List<JobAttachment>
                {
                    new JobAttachment { JobAttachmentId = Guid.NewGuid().ToString(), AttachmentName = "Prepare for implementation", CreatedBy = "Hedge Fund Principal" },
                    new JobAttachment { JobAttachmentId = Guid.NewGuid().ToString(), AttachmentName = "Deﬁne users and workﬂow", CreatedBy = "Compensation Analyst" },
                    new JobAttachment { JobAttachmentId = Guid.NewGuid().ToString(), AttachmentName = "Deﬁne the server conﬁguration.", CreatedBy = "Investment Advisor" },
                    new JobAttachment { JobAttachmentId = Guid.NewGuid().ToString(), AttachmentName = "Conﬁgure ﬁltering, if appropriate.", CreatedBy = "Actuary" }
                }
            };

            TodoTaskListViewHeightRequest = Job.ToDoTasks.Count * 60 + 38;
            CompleteTaskListViewHeightRequest = Job.CompleteTasks.Count * 60 + 40;
            AttachmentListViewHeightRequest = Job.Attachments.Count * 60 + 38;

            IsBusy = false;
        }
    }
}
