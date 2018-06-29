using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateExploreViewModel : ViewModelBase
    {
        private readonly ICandidateExploreService _candidateExploreService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private int _selectedIndex;
        private List<Vacancy> _vacancies;
        private List<Company> _companies;
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

        // height listview
        private int _recentExploreListViewHeightRequest;
        private int _jobListViewHeightRequest;
        private int _companyListViewHeightRequest;

        public CandidateExploreViewModel(ICandidateExploreService candidateExploreService, INavigationService navigationService, IDialogService dialogService)
        {
            _candidateExploreService = candidateExploreService;
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
        public List<Vacancy> Vacancies
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
        public List<Company> Companies
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
        public ICommand CancelBtnCommand => new Command(CancelExplore);
        public ICommand BackBtnCommand => new Command(RenderLandingPage);
        public ICommand FocusSearchCommand => new Command(OnFocusSearch);
        public ICommand ClearSearchCommand => new Command(OnClearSearch);

        private void RenderLandingPage()
        {
            IsBackButtonVisible = false;
            IsTriggerFocusVisible = true;
            IsCancelButtonVisible = true;
            IsFilterButtonVisible = false;
            IsSearchViewVisible = true;
            IsResultViewVisible = false;
            IsNavigationSearchVisible = true;
            CandidateMainViewModel.Current.TabHeaderMode = TabDisplayMode.NoHeader;

            CurrentExploreItem = new ExploreItem();
        }
        private async Task RecentExploreListView_ItemTappedAsync(object obj)
        {
            if (obj is ExploreItem item)
            {
                await SearchAndPopulate(item);
            }
        }
        private async Task SelectJobTabAsync()
        {
            await SearchAndPopulate(new ExploreItem { ExploreCategory = ExploreCategory.Jobs });
        }
        private async Task SelectCompanyTabAsync()
        {
            await SearchAndPopulate(new ExploreItem { ExploreCategory = ExploreCategory.Companies });
        }
        private void CancelExplore()
        {
            CandidateMainViewModel.Current.SelectTab(CandidateTab.Profile);
        }
        private void OnFocusSearch(object obj)
        {
            RenderLandingPage();
            IsBackButtonVisible = true;
            IsNavigationSearchVisible = false;
        }
        private void OnClearSearch()
        {
            CurrentExploreItem = new ExploreItem();
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

            JobListViewHeightRequest = 30;
            CompanyListViewHeightRequest = 30;
            RecentExploreListViewHeightRequest = RecentExploreItems.Count * 40 + 40;
            CandidateMainViewModel.Current.IsExplorePageRendered = true;
            await _dialogService.CloseLoadingPopup(pop);
        }

        private async Task SearchAndPopulate(ExploreItem exploreItem)
        {
            var pop = await _dialogService.OpenLoadingPopup();

            IsBackButtonVisible = true;
            IsTriggerFocusVisible = true;
            IsCancelButtonVisible = false;
            IsFilterButtonVisible = true;
            IsSearchViewVisible = false;
            IsResultViewVisible = true;
            IsNavigationSearchVisible = false;
            SelectedIndex = (int)exploreItem.ExploreCategory;
            CandidateMainViewModel.Current.TabHeaderMode = TabDisplayMode.ImageWithText;

            CurrentExploreItem = exploreItem;

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
    }

    public enum ExploreCategory
    {
        Jobs = 0,
        Companies = 1
    }
}
