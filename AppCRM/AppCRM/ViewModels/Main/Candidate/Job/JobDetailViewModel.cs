using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Tools;
using AppCRM.Utils;
using AppCRM.ViewModels.AdminArea;
using AppCRM.ViewModels.Base;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Job
{
    public class JobDetailViewModel : ViewModelBase
    {
        private readonly ICandidateJobService _candidateJobService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Guid? _vacancyID;

        private Vacancy _job;

        private int _todoTaskListViewHeightRequest;
        private int _completeTaskListViewHeightRequest;
        private int _attachmentListViewHeightRequest;

        private bool _withDrawIsVisible = true;
        private bool _applyIsVisible = false;

        private string _taskSearchedText;
        private string _documentSearchedText;

        private bool _taskNoFoundIsVisible;
        private bool _documentNoFoundIsVisible;

        private bool autoUnfocusTasksFlag = false;
        private bool autoUnfocusAttachmentFlag = false;
        private LayoutOptions _tabViewVerticalOption = LayoutOptions.FillAndExpand;

        private List<UserContactTask> _contactTasksTodo;
        private List<UserContactTask> _contactTasksComplete;
        private List<ContactDocument> _contactDocument;

        private List<UserContactTask> _contactTasksTodoList = new List<UserContactTask>();
        private List<UserContactTask> _contactTasksCompleteList = new List<UserContactTask>();
        private List<ContactDocument> _contactDocumentList = new List<ContactDocument>();
        public JobDetailViewModel(IDialogService dialogService, ICandidateJobService candidateJobService, INavigationService navigationService)
        {
            _dialogService = dialogService;
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

        public List<UserContactTask> ContactTasksTodo
        {
            get
            {
                return _contactTasksTodo;
            }
            set
            {
                _contactTasksTodo = value;
                OnPropertyChanged();
            }
        }
        public List<UserContactTask> ContactTasksComplete
        {
            get
            {
                return _contactTasksComplete;
            }
            set
            {
                _contactTasksComplete = value;
                OnPropertyChanged();
            }
        }
        public List<ContactDocument> ContactDocument
        {
            get
            {
                return _contactDocument;
            }
            set
            {
                _contactDocument = value;
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

        public bool WithDrawIsVisible
        {
            get
            {
                return _withDrawIsVisible;
            }
            set
            {
                _withDrawIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool ApplyIsVisible
        {
            get
            {
                return _applyIsVisible;
            }
            set
            {
                _applyIsVisible = value;
                OnPropertyChanged();
            }
        }

        public string TaskSearchedText
        {
            get
            {
                return _taskSearchedText;
            }
            set
            {
                _taskSearchedText = value;
                TaskSearchCommandExecute(_taskSearchedText);
                OnPropertyChanged();
            }
        }
        public string DocumentSearchedText
        {
            get
            {
                return _documentSearchedText;
            }
            set
            {
                _documentSearchedText = value;
                DocumentSearchCommandExecute(_documentSearchedText);
                OnPropertyChanged();
            }
        }

        public bool TaskNoFoundIsVisible
        {
            get
            {
                return _taskNoFoundIsVisible;
            }
            set
            {
                _taskNoFoundIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool DocumentNoFoundIsVisible
        {
            get
            {
                return _documentNoFoundIsVisible;
            }
            set
            {
                _documentNoFoundIsVisible = value;
                OnPropertyChanged();
            }
        }

        public LayoutOptions TabViewVerticalOption
        {
            get
            {
                return _tabViewVerticalOption;
            }
            set
            {
                _tabViewVerticalOption = value;
                OnPropertyChanged();
            }
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand WithDrawComand => new AsyncCommand(WithDrawComandAsync);
        public ICommand ApplyComand => new AsyncCommand(ApplyComandAsync);
        public ICommand SearchTasksFocusChangedCommand => new Command(OnSearchTasksFocusChanged);
        public ICommand SearchAttachmentFocusChangedCommand => new Command(OnSearchAttachmentFocusChanged);

        private void OnSearchTasksFocusChanged(object obj)
        {
            if (obj is FocusEventArgs focusEventArgs)
            {
                var element = focusEventArgs.VisualElement;
                if (focusEventArgs.IsFocused)
                {
                    if (TabViewVerticalOption.Alignment != LayoutAlignment.Start)
                    {
                        TabViewVerticalOption = LayoutOptions.Start;
                        autoUnfocusTasksFlag = true;
                    }
                }
                else
                {
                    if (autoUnfocusTasksFlag)
                    {
                        autoUnfocusTasksFlag = false;
                        element.Focus();
                    }
                    else
                    {
                        TabViewVerticalOption = LayoutOptions.FillAndExpand;
                    }
                }
            }
        }
        private void OnSearchAttachmentFocusChanged(object obj)
        {
            if (obj is FocusEventArgs focusEventArgs)
            {
                var element = focusEventArgs.VisualElement;
                if (focusEventArgs.IsFocused)
                {
                    if (TabViewVerticalOption.Alignment != LayoutAlignment.Start)
                    {
                        TabViewVerticalOption = LayoutOptions.Start;
                        autoUnfocusAttachmentFlag = true;
                    }
                }
                else
                {
                    if (autoUnfocusAttachmentFlag)
                    {
                        autoUnfocusAttachmentFlag = false;
                        element.Focus();
                    }
                    else
                    {
                        TabViewVerticalOption = LayoutOptions.FillAndExpand;
                    }
                }
            }
        }
        private async Task BtnBackAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            WithDrawIsVisible = true;
            ApplyIsVisible = false;

            var pop = await _dialogService.OpenLoadingPopup();
            _vacancyID = (Guid)navigationData;
            dynamic obj = await _candidateJobService.GetVacancyDetails(_vacancyID);
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
                List<JobRequire> requires;
                if (obj["requiredList"] != null)
                {
                    requires = JsonConvert.DeserializeObject<List<JobRequire>>(obj["requiredList"].ToString());
                }
                else
                {
                    requires = new List<JobRequire>();
                }

                //Get contact Task
                dynamic objContactTask = await _candidateJobService.GetContactTaskByContactIDAndVacancyID(_vacancyID);

                List<UserContactTask> contactTasksTodo = new List<UserContactTask>();
                List<UserContactTask> contactTasksComplete = new List<UserContactTask>();
                if (objContactTask["contactTasks"] != null)
                {
                    List<UserContactTask> ContactTasks = JsonConvert.DeserializeObject<List<UserContactTask>>(objContactTask["contactTasks"].ToString());
                    foreach (var contactTask in ContactTasks)
                    {
                        if (contactTask.Completed)
                        {
                            contactTasksComplete.Add(contactTask);
                        }
                        else
                        {
                            contactTasksTodo.Add(contactTask);
                        }
                    }
                }

                _contactTasksTodoList.AddRange(contactTasksTodo);
                _contactTasksCompleteList.AddRange(contactTasksComplete);

                ContactTasksComplete = contactTasksComplete;
                ContactTasksTodo = contactTasksTodo;

                //Get contact Document
                dynamic objContactDocument = await _candidateJobService.GetDocumentsAssigneedByContactIDAndVacancyID(_vacancyID);
                List<ContactDocument> contactDocuments;
                if (objContactDocument["contactDocuments"] != null)
                {
                    contactDocuments = JsonConvert.DeserializeObject<List<ContactDocument>>(objContactDocument["contactDocuments"].ToString());
                }
                else
                {
                    contactDocuments = new List<ContactDocument>();
                }

                _contactDocumentList.AddRange(contactDocuments);
                ContactDocument = contactDocuments;

                Job = new Vacancy
                {
                    VacancyID = _vacancyID,
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
                    //ContactTasksTodo = contactTasksTodo,
                    //ContactTasksComplete = contactTasksComplete,
                    //ContactDocuments = contactDocuments
                };

                TodoTaskListViewHeightRequest = ContactTasksTodo.Count * 60 + 38;
                CompleteTaskListViewHeightRequest = ContactTasksComplete.Count * 60 + 40;
                AttachmentListViewHeightRequest = ContactDocument.Count * 60 + 38;
            }
            await _dialogService.CloseLoadingPopup(pop);
        }

        private void TaskSearchCommandExecute(string _search)
        {
            ContactTasksTodo.RemoveRange(0, ContactTasksTodo.Count);
            ContactTasksComplete.RemoveRange(0, ContactTasksComplete.Count);

            List<UserContactTask> contactTaskTodo = new List<UserContactTask>();
            foreach (var item in _contactTasksTodoList)
            {
                if (item.TaskName.ToLowerInvariant().Contains(_search.ToLowerInvariant()))
                {
                    contactTaskTodo.Add(item);
                }
            }
            ContactTasksTodo = contactTaskTodo;

            List<UserContactTask> contactTaskcomplete = new List<UserContactTask>();
            foreach (var item in _contactTasksCompleteList)
            {
                if (item.TaskName.ToLowerInvariant().Contains(_search.ToLowerInvariant()))
                {
                    contactTaskcomplete.Add(item);
                }
            }
            ContactTasksComplete = contactTaskcomplete;

            if (ContactTasksComplete.Count == 0 && ContactTasksTodo.Count == 0)
            {
                TaskNoFoundIsVisible = true;
            }
            else
            {
                TaskNoFoundIsVisible = false;
            }

            TodoTaskListViewHeightRequest = ContactTasksTodo.Count * 60 + 38;
            CompleteTaskListViewHeightRequest = ContactTasksComplete.Count * 60 + 40;
        }

        private void DocumentSearchCommandExecute(string _search)
        {
            ContactDocument.RemoveRange(0, ContactDocument.Count);

            List<ContactDocument> contactDocument = new List<ContactDocument>();
            foreach (var item in _contactDocumentList)
            {
                if (item.DocumentName.ToLowerInvariant().Contains(_search.ToLowerInvariant()))
                {
                    contactDocument.Add(item);
                }
            }
            ContactDocument = contactDocument;

            if (ContactDocument.Count == 0)
            {
                DocumentNoFoundIsVisible = true;
            }
            else
            {
                DocumentNoFoundIsVisible = false;
            }

            AttachmentListViewHeightRequest = ContactDocument.Count * 60 + 38;
        }

        public async Task WithDrawComandAsync()
        {
            var result = await _dialogService.Alert("Please confirm you wish to withdraw your application?", "All related information will be removed from system", "Confirm Withdraw", "Cancel");
            if (result)
            {
                var pop = await _dialogService.OpenLoadingPopup();
                var obj = await _candidateJobService.WithDrawVacancy(_vacancyID);
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _dialogService.PopupMessage("WithDraw Successefully", "#52CD9F", "#FFFFFF");
                        WithDrawIsVisible = false;
                        ApplyIsVisible = true;
                    }
                    else if (obj["Success"] == "false")
                    {
                        await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                    await _dialogService.CloseLoadingPopup(pop);
                }
                await _dialogService.CloseLoadingPopup(pop);
            }
        }

        public async Task ApplyComandAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            var obj = await _candidateJobService.ApplyVacancy(_vacancyID);
            try
            {
                if (obj["Success"] == "true") //success
                {
                    await _dialogService.PopupMessage("Apply Job Successefully", "#52CD9F", "#FFFFFF");
                    WithDrawIsVisible = true;
                    ApplyIsVisible = false;
                }
                else if (obj["Success"] == "false")
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                }
            }
            catch
            {
                await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                await _dialogService.CloseLoadingPopup(pop);
            }
            await _dialogService.CloseLoadingPopup(pop);
        }
    }
}
