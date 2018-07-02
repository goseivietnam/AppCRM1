using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Employer;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate.Explore;
using Syncfusion.XForms.TabView;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateExploreViewModel : ViewModelBase
    {
        private readonly ICandidateExploreService _candidateExploreService;
        private readonly IEmployerJobService _employerJobService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private int _selectedIndex;
        private List<ContactJobs> _vacancies;
        private List<Models.Account> _companies;
        private List<ExploreItem> _recentExploreItems;
        private ExploreItem _currentExploreItem;

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

        // height listview
        private int _recentExploreListViewHeightRequest;
        private int _jobListViewHeightRequest;
        private int _companyListViewHeightRequest;

        public CandidateExploreViewModel(ICandidateExploreService candidateExploreService, IEmployerJobService employerJobService, INavigationService navigationService, IDialogService dialogService)
        {
            _candidateExploreService = candidateExploreService;
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
        public List<ContactJobs> Vacancies
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
        public List<Models.Account> Companies
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

        public override async Task InitializeAsync(object navigationData)
        {
            var pop = await _dialogService.OpenLoadingPopup();

            RenderLandingPage();
            SelectedIndex = 0;

            RecentExploreItems = new List<ExploreItem>
            {
                new ExploreItem { Title = "Marketing", ExploreCategory = ExploreCategory.Companies },
                new ExploreItem { Title = "Marketing", Location = "Queensland" },
                new ExploreItem { Title = "ios developer", Location = "Queensland" }
            };

            //Companies = new List<Models.Account>();
            RecentExploreListViewHeightRequest = RecentExploreItems.Count * 40 + 40;

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

            SearchParameters parameters = new SearchParameters { CurrentPage = 1, JobTotal = 10 };
            dynamic obj = await _candidateExploreService.GetCandidateJobsSearch(parameters);
            if (obj["Jobs"] != null)
            {
                Vacancies = JsonConvert.DeserializeObject<List<ContactJobs>>(obj["Jobs"].ToString());
            }
            else
            {
                Vacancies = new List<ContactJobs>();
            }

            EmployerSearchFilter filter = new EmployerSearchFilter {
                KeySearch1 = CurrentExploreItem.Title,
                KeySearch2 = CurrentExploreItem.Location,
                CurrentPage = 1,
                PageSize = 10 };
            dynamic objEmployerlist = await _employerJobService.GetEmployerList(filter);
            if (objEmployerlist["employers"] != null)
            {
                Companies = JsonConvert.DeserializeObject<List<Models.Account>>(objEmployerlist["employers"].ToString());
            }
            else
            {
                Companies = new List<Models.Account>();
            }

            JobListViewHeightRequest = Vacancies.Count * 100;
            CompanyListViewHeightRequest = Companies.Count * 100;
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
