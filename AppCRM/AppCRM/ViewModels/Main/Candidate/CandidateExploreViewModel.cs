using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Employer;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate.Explore;
using Newtonsoft.Json;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.TabView;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateExploreViewModel : ViewModelBase
    {
        private readonly ICandidateExploreService _candidateExploreService;
        private readonly ICandidateJobService _candidateJobService;
        private readonly IEmployerJobService _employerJobService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly int PageSize = 5;

        private int _selectedIndex;
        private ObservableCollection<ContactJobs> _vacancies;
        private ObservableCollection<Models.Account> _companies;
        private List<ExploreItem> _recentExploreItems;
        private ExploreItem _currentExploreItem;

        public SearchParameters FilterParameters = new SearchParameters();
        public EmployerSearchFilter FilterEmployer = new EmployerSearchFilter();

        //component visibility
        private bool _isBackButtonVisible;
        private bool _isTriggerFocusVisible;
        private bool _isCancelButtonVisible;
        private bool _isFilterButtonVisible;
        private bool _isSearchViewVisible;
        private bool _isResultViewVisible;
        private bool _isNavigationSearchVisible;
        private bool _isSearchDetailVisible;

        private bool _isSearchFocused;
        private bool _isTitleSearchFocused;
        private bool _isLocationSearchFocused;

        private bool _loadMoreIsVisible;
        private bool _companiesLoadMoreIsVisible;


        // height listview
        private int _recentExploreListViewHeightRequest;
        private int _companyListViewHeightRequest;

        private ContactJobs SwipedJobItem = new ContactJobs();

        public CandidateExploreViewModel(ICandidateExploreService candidateExploreService, ICandidateJobService candidateJobService, IEmployerJobService employerJobService, INavigationService navigationService, IDialogService dialogService)
        {
            _candidateExploreService = candidateExploreService;
            _candidateJobService = candidateJobService;
            _employerJobService = employerJobService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ContactJobs> Vacancies
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
        public ObservableCollection<Models.Account> Companies
        {
            get
            {
                return _companies;
            }
            set
            {
                _companies = value;
                OnPropertyChanged();
            }
        }
        public List<ExploreItem> RecentExploreItems
        {
            get
            {
                return _recentExploreItems;
            }
            set
            {
                _recentExploreItems = value;
                OnPropertyChanged();
            }
        }
        public ExploreItem CurrentExploreItem
        {
            get
            {
                return _currentExploreItem;
            }
            set
            {
                _currentExploreItem = value;
                OnPropertyChanged();
            }
        }

        public bool IsBackButtonVisible
        {
            get
            {
                return _isBackButtonVisible;
            }
            set
            {
                _isBackButtonVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsTriggerFocusVisible
        {
            get
            {
                return _isTriggerFocusVisible;
            }
            set
            {
                _isTriggerFocusVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsCancelButtonVisible
        {
            get
            {
                return _isCancelButtonVisible;
            }
            set
            {
                _isCancelButtonVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsFilterButtonVisible
        {
            get
            {
                return _isFilterButtonVisible;
            }
            set
            {
                _isFilterButtonVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsSearchViewVisible
        {
            get
            {
                return _isSearchViewVisible;
            }
            set
            {
                _isSearchViewVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsResultViewVisible
        {
            get
            {
                return _isResultViewVisible;
            }
            set
            {
                _isResultViewVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsNavigationSearchVisible
        {
            get
            {
                return _isNavigationSearchVisible;
            }
            set
            {
                _isNavigationSearchVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsSearchDetailVisible
        {
            get
            {
                return _isSearchDetailVisible;
            }
            set
            {
                _isSearchDetailVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsSearchFocused
        {
            get
            {
                return _isSearchFocused;
            }
            set
            {
                _isSearchFocused = value;
                OnPropertyChanged();
            }
        }
        public bool IsTitleSearchFocused
        {
            get
            {
                return _isTitleSearchFocused;
            }
            set
            {
                _isTitleSearchFocused = value;
                OnPropertyChanged();
            }
        }
        public bool IsLocationSearchFocused
        {
            get
            {
                return _isLocationSearchFocused;
            }
            set
            {
                _isLocationSearchFocused = value;
                OnPropertyChanged();
            }
        }

        public bool LoadMoreIsVisible
        {
            get
            {
                return _loadMoreIsVisible;
            }
            set
            {
                _loadMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool CompaniesLoadMoreIsVisible
        {
            get
            {
                return _companiesLoadMoreIsVisible;
            }
            set
            {
                _companiesLoadMoreIsVisible = value;
                OnPropertyChanged();
            }
        }
        public int RecentExploreListViewHeightRequest
        {
            get
            {
                return _recentExploreListViewHeightRequest;
            }
            set
            {
                _recentExploreListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }
        public int CompanyListViewHeightRequest
        {
            get
            {
                return _companyListViewHeightRequest;
            }
            set
            {
                _companyListViewHeightRequest = value;
                OnPropertyChanged();
            }
        }

        public ICommand RecentExploreListViewCommand => new AsyncCommand(RecentExploreListView_ItemTappedAsync);
        public ICommand JobsBtnCommand => new AsyncCommand(SelectJobTabAsync);
        public ICommand CompaniesBtnCommand => new AsyncCommand(SelectCompanyTabAsync);
        public ICommand CancelBtnCommand => new Command(CancelSearch);
        public ICommand BackBtnCommand => new Command(RenderLandingPage);
        public ICommand FocusSearchCommand => new Command(OnFocusSearch);
        public ICommand ClearSearchCommand => new Command(OnClearSearch);
        public ICommand ClearTitleSearchCommand => new Command(OnClearTitleSearch);
        public ICommand ClearLocationSearchCommand => new Command(OnClearLocationSearch);
        public ICommand SearchCompletedCommand => new AsyncCommand(OnSearchCompletedAsync);
        public ICommand FilterBtnCommand => new AsyncCommand(OnFilterBtn);
        public ICommand JobTappedCommand => new AsyncCommand(OpenJobDetail);
        public ICommand CompanyTappedCommand => new AsyncCommand(OpenCompanyDetail);
        public ICommand SwipeJobItemCommand => new Command(SwipeJobItem);
        public ICommand ShortListChangedCommand => new Command(AddShortListTapGestureRecognizer);
        public ICommand ApplyChangedCommand => new Command(AddApplyTapGestureRecognizer);
        public ICommand WithDrawChangedCommand => new Command(WithDrawTapGestureRecognizer);
        public ICommand LoadMoreVacanciesCommand => new AsyncCommand(LoadMoreVacancies);
        public ICommand LoadMoreCompaniesCommand => new AsyncCommand(LoadMoreCompanies);

        private void RenderLandingPage()
        {
            IsBackButtonVisible = false;
            IsTriggerFocusVisible = true;
            IsCancelButtonVisible = true;
            IsFilterButtonVisible = false;
            IsSearchViewVisible = true;
            IsResultViewVisible = false;
            IsNavigationSearchVisible = true;
            IsSearchDetailVisible = false;

            FilterParameters.CurrentPage = 1;
            FilterEmployer.CurrentPage = 1;

            CandidateMainViewModel.Current.TabHeaderMode = TabDisplayMode.NoHeader;

            CurrentExploreItem = new ExploreItem();
        }
        private async Task RecentExploreListView_ItemTappedAsync(object obj)
        {
            if (obj is ExploreItem item)
            {
                CurrentExploreItem = new ExploreItem(item);
                await SearchAndPopulate();
            }
        }
        private async Task SelectJobTabAsync()
        {
            CurrentExploreItem = new ExploreItem { ExploreCategory = ExploreCategory.Jobs };
            await SearchAndPopulate();
        }
        private async Task SelectCompanyTabAsync()
        {
            CurrentExploreItem = new ExploreItem { ExploreCategory = ExploreCategory.Companies };
            await SearchAndPopulate();
        }
        private void CancelSearch()
        {
            if (!IsTriggerFocusVisible)
            {
                RenderLandingPage();
            }
            else
            {
                CandidateMainViewModel.Current.SelectTab(CandidateTab.Profile);
            }
        }
        private void OnFocusSearch(object obj)
        {
            if (obj != null && obj is bool && (bool)obj)
            {
                IsBackButtonVisible = true;
                IsTriggerFocusVisible = false;
                IsCancelButtonVisible = true;
                IsFilterButtonVisible = false;
                IsSearchViewVisible = true;
                IsResultViewVisible = false;
                IsNavigationSearchVisible = false;

                IsSearchFocused = false;
                IsSearchDetailVisible = true;
                IsTitleSearchFocused = true;
            }
        }
        private void OnClearSearch()
        {
            CurrentExploreItem = new ExploreItem();
            IsSearchFocused = true;
        }
        private void OnClearTitleSearch()
        {
            CurrentExploreItem.Title = string.Empty;
            CurrentExploreItem = new ExploreItem(CurrentExploreItem);
            IsTitleSearchFocused = true;
        }
        private void OnClearLocationSearch()
        {
            CurrentExploreItem.Location = string.Empty;
            CurrentExploreItem = new ExploreItem(CurrentExploreItem);
            IsLocationSearchFocused = true;
        }
        private async Task OnSearchCompletedAsync(object obj)
        {
            await SearchAndPopulate();
        }
        private async Task OnFilterBtn()
        {
            await _navigationService.NavigateToPopupAsync<FiltersViewModel>(true);
        }
        private async Task OpenJobDetail(object obj)
        {
            var job = (obj as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as ContactJobs;
            await _navigationService.NavigateToPopupAsync<JobDetailViewModel>(job.VacancyID, true);
        }
        private async Task OpenCompanyDetail(object obj)
        {
            var company = (obj as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as Models.Account;
            await _navigationService.NavigateToPopupAsync<CompanyDetailViewModel>(company.AccountID, true);
        }
        private void SwipeJobItem(object obj)
        {
            SwipedJobItem = (obj as SwipeEndedEventArgs).ItemData as ContactJobs;
        }
        private void AddShortListTapGestureRecognizer(object sender)
        {
            (sender as Button).Command = new AsyncCommand(ShortListJob);
        }
        private void AddApplyTapGestureRecognizer(object sender)
        {
            (sender as Button).Command = new AsyncCommand(ApplyJob);
        }
        private void WithDrawTapGestureRecognizer(object sender)
        {
            (sender as Button).Command = new AsyncCommand(WithDrawJob);
        }
        private async Task ShortListJob()
        {
            if (SwipedJobItem.VacancyID.HasValue)
            {
                var pop = await _dialogService.OpenLoadingPopup();
                bool shortlisted;
                if (SwipedJobItem.Status == "Interested") { shortlisted = true; } else { shortlisted = false; }
                var obj = await _candidateJobService.ShortListJob(shortlisted, SwipedJobItem.VacancyID);

                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        ContactJobs oldValue = Vacancies.Where(x => x.VacancyID == SwipedJobItem.VacancyID).FirstOrDefault();
                        if (obj["Message"] == "UnShortlist")
                        {
                            oldValue.Status = null;
                            await _dialogService.PopupMessage("Remove Shortlist Job Successefully", "#52CD9F", "#FFFFFF");
                        }
                        else if (obj["Message"] == "Shortlist")
                        {
                            oldValue.Status = "Interested";
                            await _dialogService.PopupMessage("Shortlist Job Successefully", "#52CD9F", "#FFFFFF");
                        }
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
        private async Task ApplyJob()
        {
            if (SwipedJobItem.VacancyID.HasValue)
            {
                var pop = await _dialogService.OpenLoadingPopup();

                var obj = await _candidateJobService.ApplyVacancy(SwipedJobItem.VacancyID);
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _dialogService.PopupMessage("Apply Job Successefully", "#52CD9F", "#FFFFFF");

                        Vacancies.FirstOrDefault(x => x.VacancyID == SwipedJobItem.VacancyID).Status = "Applied";
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
        public async Task WithDrawJob()
        {
            if (SwipedJobItem.VacancyID.HasValue)
            {
                var result = await _dialogService.Alert("Please confirm you wish to withdraw your application?", "All related information will be removed from system", "Confirm Withdraw", "Cancel");
                if (result)
                {
                    var pop = await _dialogService.OpenLoadingPopup();
                    var obj = await _candidateJobService.WithDrawVacancy(SwipedJobItem.VacancyID);
                    try
                    {
                        if (obj["Success"] == "true") //success
                        {
                            await _dialogService.PopupMessage("WithDraw Successefully", "#52CD9F", "#FFFFFF");

                            Vacancies.FirstOrDefault(x => x.VacancyID == SwipedJobItem.VacancyID).Status = null;
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
        private async Task LoadMoreVacancies()
        {
            IsBusy = true;
            LoadMoreIsVisible = false;
            FilterParameters.CurrentPage += 1;
            dynamic obj = await _candidateExploreService.GetCandidateJobsSearch(FilterParameters);
            if (obj["Jobs"] != null)
            {
                List<ContactJobs> listMore = JsonConvert.DeserializeObject<List<ContactJobs>>(obj["Jobs"].ToString());
                foreach (var item in listMore)
                {
                    Vacancies.Add(item);
                }
                if (listMore.Count < PageSize)
                {
                    LoadMoreIsVisible = false;
                }
                else
                {
                    LoadMoreIsVisible = true;
                }
            }
            else
            {
                FilterParameters.CurrentPage--;
                LoadMoreIsVisible = true;
            }
            IsBusy = false;
        }
        private async Task LoadMoreCompanies()
        {
            IsBusy = true;
            CompaniesLoadMoreIsVisible = false;
            //FilterEmployer.KeySearch1 = CurrentExploreItem.Title;
            //FilterEmployer.KeySearch2 = CurrentExploreItem.Location;
            FilterEmployer.CurrentPage += 1;
            dynamic objEmployerlist = await _employerJobService.GetEmployerList(FilterEmployer);
            if (objEmployerlist["employers"] != null)
            {
                List<Models.Account> listMore = JsonConvert.DeserializeObject<List<Models.Account>>(objEmployerlist["employers"].ToString());
                foreach (var item in listMore)
                {
                    Companies.Add(item);
                }
                if (listMore.Count < PageSize)
                {
                    CompaniesLoadMoreIsVisible = false;
                }
                else
                {
                    CompaniesLoadMoreIsVisible = true;
                }
            }
            else
            {
                FilterEmployer.CurrentPage--;
                CompaniesLoadMoreIsVisible = true;
            }
            IsBusy = false;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var pop = await _dialogService.OpenLoadingPopup();
            LoadMoreIsVisible = false;
            CompaniesLoadMoreIsVisible = false;

            RenderLandingPage();
            SelectedIndex = 0;

            RecentExploreItems = new List<ExploreItem>
            {
                new ExploreItem { Title = "Marketing", ExploreCategory = ExploreCategory.Companies },
                new ExploreItem { Title = "Marketing", Location = "Queensland" },
                new ExploreItem { Title = "ios developer", Location = "Queensland" }
            };

            RecentExploreListViewHeightRequest = RecentExploreItems.Count * 40 + 40;

            LoadMoreIsVisible = false;
            CompaniesLoadMoreIsVisible = false;
            CandidateMainViewModel.Current.IsExplorePageRendered = true;
            await _dialogService.CloseLoadingPopup(pop);
        }

        private async Task SearchAndPopulate()
        {
            var pop = await _dialogService.OpenLoadingPopup();

            IsBackButtonVisible = true;
            IsTriggerFocusVisible = true;
            IsCancelButtonVisible = false;
            IsFilterButtonVisible = true;
            IsSearchViewVisible = false;
            IsResultViewVisible = true;
            IsNavigationSearchVisible = false;
            IsSearchDetailVisible = false;
            SelectedIndex = (int)CurrentExploreItem.ExploreCategory;
            CandidateMainViewModel.Current.TabHeaderMode = TabDisplayMode.ImageWithText;
            CurrentExploreItem = new ExploreItem(CurrentExploreItem);

            //Get Vacancies
            FilterParameters.Keyword = CurrentExploreItem.Title;
            FilterParameters.Location = CurrentExploreItem.Location;
            dynamic obj = await _candidateExploreService.GetCandidateJobsSearch(FilterParameters);
            if (obj["Jobs"] != null)
            {
                Vacancies = JsonConvert.DeserializeObject<ObservableCollection<ContactJobs>>(obj["Jobs"].ToString());
            }
            else
            {
                Vacancies = new ObservableCollection<ContactJobs>();
            }

            if (Vacancies.Count < PageSize)
            {
                LoadMoreIsVisible = false;
            }
            else
            {
                LoadMoreIsVisible = true;
            }

            //Get Companies
            FilterEmployer.KeySearch1 = CurrentExploreItem.Title;
            FilterEmployer.KeySearch2 = CurrentExploreItem.Location;

            dynamic objEmployerlist = await _employerJobService.GetEmployerList(FilterEmployer);
            if (objEmployerlist["employers"] != null)
            {
                Companies = JsonConvert.DeserializeObject<ObservableCollection<Models.Account>>(objEmployerlist["employers"].ToString());
            }
            else
            {
                Companies = new ObservableCollection<Models.Account>();
            }

            if (Companies.Count < PageSize)
            {
                CompaniesLoadMoreIsVisible = false;
            }
            else
            {
                CompaniesLoadMoreIsVisible = true;
            }

            await _dialogService.CloseLoadingPopup(pop);
        }
    }

    public class ExploreItem
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public ExploreCategory ExploreCategory { get; set; } = ExploreCategory.Jobs;

        public bool IsTitleWithLocation { get { return !string.IsNullOrEmpty(this.Title) && !string.IsNullOrEmpty(this.Location); } }
        public string Display
        {
            get
            {
                if (string.IsNullOrEmpty(this.Title))
                {
                    if (string.IsNullOrEmpty(this.Location))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return this.Location;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.Location))
                    {
                        return this.Title;
                    }
                    else
                    {
                        return this.Title + " in " + this.Location;
                    }
                }
            }
        }

        public ExploreItem()
        {
        }

        public ExploreItem(ExploreItem obj)
        {
            this.Title = obj.Title;
            this.Location = obj.Location;
            this.ExploreCategory = obj.ExploreCategory;
        }
    }

    public enum ExploreCategory
    {
        Jobs = 0,
        Companies = 1
    }
}
