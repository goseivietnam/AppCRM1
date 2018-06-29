﻿using AppCRM.Models;
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

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand ListViewCommand => new Command(ListViewTapped);
        public ICommand WithDrawComand => new AsyncCommand(WithDrawComandAsync);
        public ICommand ApplyComand => new AsyncCommand(ApplyComandAsync);

        private async Task BtnBackAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        private void ListViewTapped()
        {

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
                    ContactTasksTodo = contactTasksTodo,
                    ContactTasksComplete = contactTasksComplete,
                    ContactDocuments = contactDocuments
                };

                TodoTaskListViewHeightRequest = Job.ContactTasksTodo.Count * 60 + 38;
                CompleteTaskListViewHeightRequest = Job.ContactTasksComplete.Count * 60 + 40;
                AttachmentListViewHeightRequest = Job.ContactDocuments.Count * 60 + 38;
            }
            await _dialogService.CloseLoadingPopup(pop);
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