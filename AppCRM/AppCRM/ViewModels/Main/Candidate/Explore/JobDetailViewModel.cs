using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Tools;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Explore
{
    public class JobDetailViewModel : ViewModelBase
    {
        private readonly ICandidateExploreService _candidateExploreService;
        private readonly ICandidateJobService _candidateJobService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Guid? _vacancyID;

        private Vacancy _job;

        public JobDetailViewModel(IDialogService dialogService, ICandidateExploreService candidateExploreService, ICandidateJobService candidateJobService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateExploreService = candidateExploreService;
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
                };
            }
            await _dialogService.CloseLoadingPopup(pop);
        }
    }
}
