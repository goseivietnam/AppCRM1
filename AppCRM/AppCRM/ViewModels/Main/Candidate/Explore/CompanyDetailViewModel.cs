using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Employer;
using AppCRM.Services.Navigation;
using AppCRM.Tools;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Explore
{
    public class CompanyDetailViewModel : ViewModelBase
    {
        private readonly ICandidateExploreService _candidateExploreService;
        private readonly IEmployerJobService _employerJobService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Guid _accountID;
        private int _currentPageVacancies = 1;
        private readonly int PageSize = 5;

        private Models.Account _company;
        private ObservableCollection<AccountJobs> _vacancies;

        private bool _vacancisLoadMoreIsVisible;

        public CompanyDetailViewModel(IDialogService dialogService, IEmployerJobService employerJobService, ICandidateExploreService candidateExploreService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateExploreService = candidateExploreService;
            _employerJobService = employerJobService;
            _navigationService = navigationService;
        }

        public Models.Account Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AccountJobs> Vacancies
        {
            get
            {
                return _vacancies;
            }
            set
            {
                _vacancies = value;
                OnPropertyChanged();
            }
        }
        public bool VacancisLoadMoreIsVisible
        {
            get
            {
                return _vacancisLoadMoreIsVisible;
            }
            set
            {
                _vacancisLoadMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand ListViewCommand => new Command(ListViewTapped);
        public ICommand LoadMoreVacanciesCommand => new AsyncCommand(LoadMoreVacancies);

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
            _accountID = (Guid)navigationData;

            dynamic obj = await _employerJobService.GetEmployerDetails(_accountID);

            if (obj["EmployerDetails"] != null)
            {
                string aboutUs = "";
                if (obj["EmployerDetails"]["AboutUs"] != null)
                {
                    aboutUs = Utilities.HtmlToPlainText(obj["EmployerDetails"]["AboutUs"].ToString());
                }

                Company = new Models.Account
                {
                    AccountID = _accountID,
                    AboutUs = aboutUs,
                    AccountName = obj["EmployerDetails"]["AccountName"],
                    WebSite = obj["EmployerDetails"]["WebSite"],
                    Address = obj["EmployerDetails"]["Address"],
                    VideoLink = obj["EmployerDetails"]["VideoLink"]
                };
            }

            AccountJobs AJ = new AccountJobs
            {
                AccountID = _accountID,
                CurrentPage = _currentPageVacancies,
                PageSize = PageSize
            };
            dynamic objVacancies = await _employerJobService.GetRelatedJobs(AJ);
            if (objVacancies["records"] != null)
            {
                Vacancies = JsonConvert.DeserializeObject<ObservableCollection<AccountJobs>>(objVacancies["records"].ToString());
            }
            VacancisLoadMoreIsVisible = true;

            if (objVacancies["hasMore"] == false)
            {
                VacancisLoadMoreIsVisible = false;
            }
            await _dialogService.CloseLoadingPopup(pop);
        }

        private async Task LoadMoreVacancies()
        {
            IsBusy = true;

            _currentPageVacancies += 1;
            AccountJobs AJ = new AccountJobs
            {
                AccountID = _accountID,
                CurrentPage = _currentPageVacancies,
                PageSize = PageSize
            };

            dynamic obj = await _employerJobService.GetRelatedJobs(AJ);

            if (obj["Jobs"] != null)
            {
                List<AccountJobs> listMore = JsonConvert.DeserializeObject<List<AccountJobs>>(obj["records"].ToString());
                if (listMore.Count > 0)
                {
                    foreach (var item in listMore)
                    {
                        Vacancies.Add(item);
                    }
                    if (obj["hasMore"] == false)
                    {
                        VacancisLoadMoreIsVisible = false;
                    }
                }
                else
                {
                    VacancisLoadMoreIsVisible = false;
                }
            }
            IsBusy = false;
        }
    }
}
