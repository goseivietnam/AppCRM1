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
using System.Linq;
using System.Text.RegularExpressions;
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
        private string _shorlistText;
        private double _videoHeight;
        private string _videoUrl;

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
        public double VideoHeight
        {
            get { return _videoHeight; }
            set { _videoHeight = value; OnPropertyChanged(); }
        }
        public string VideoUrl
        {
            get { return _videoUrl; }
            set { _videoUrl = value; OnPropertyChanged(); }
        }
        public string ShorlistText
        {
            get { return _shorlistText; }
            set { _shorlistText = value; OnPropertyChanged(); }
        }
        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand ListViewCommand => new Command(ListViewTapped);
        public ICommand LoadMoreVacanciesCommand => new AsyncCommand(LoadMoreVacancies);
        public ICommand ShortlistCommand => new AsyncCommand(ShortlistCommandAsync);

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
                    VideoLink = obj["EmployerDetails"]["VideoLink"],
                    FavouriteEmployerID = obj["EmployerDetails"]["FavouriteEmployerID"]
                };

                ShorlistText = obj["EmployerDetails"]["FavouriteEmployerID"] == null ? "♡ Shortlist" : "♡ Remove Shortlist";
            }

            AccountJobs AJ = new AccountJobs
            {
                AccountID = _accountID,
                CurrentPage = _currentPageVacancies,
                PageSize = PageSize
            };
            Dictionary<string, object> objVacancies = await _employerJobService.GetRelatedJobs(AJ);
            if (objVacancies["records"] != null)
            {
                Vacancies = JsonConvert.DeserializeObject<ObservableCollection<AccountJobs>>(objVacancies["records"].ToString());
            }
            VacancisLoadMoreIsVisible = true;

            if (objVacancies["hasMore"].ToString() == "false")
            {
                VacancisLoadMoreIsVisible = false;
            }

            VideoHeight = Application.Current.MainPage.Width / 16 * 9;
            VideoUrl = GetYoutubeEmbed(Company.VideoLink);
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

        private async Task ShortlistCommandAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            bool IsFavourited;
            if (Company.FavouriteEmployerID != null) { IsFavourited = true; } else { IsFavourited = false; }
            Dictionary<string, object> obj = await _candidateExploreService.AddRemoveFavouriteEmployer(IsFavourited, Company.AccountID);

            try
            {
                if (obj["Success"].ToString() == "true") //success
                {
                    if (obj["Message"].ToString() == "RemoveFavourite")
                    {
                        Company.FavouriteEmployerID = null;
                        ShorlistText = "♡ Shortlist";
                         await _dialogService.PopupMessage("Remove Favourite Successefully", "#52CD9F", "#FFFFFF");
                    }
                    else if (obj["Message"].ToString() == "Shortlist")
                    {
                        Company.FavouriteEmployerID = Company.AccountID;
                        ShorlistText = "♡ Shortlist";
                        await _dialogService.PopupMessage("Add Favourite Successefully", "#52CD9F", "#FFFFFF");
                    }
                }
                else if (obj["Success"].ToString() == "false")
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
        private string GetYoutubeEmbed(string url)
        {
            var youtubeMatch = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)").Match(url);
            return string.Format(@"https://www.youtube.com/embed/{0}", youtubeMatch.Success ? youtubeMatch.Groups[1].Value : string.Empty);
        }
    }
}
