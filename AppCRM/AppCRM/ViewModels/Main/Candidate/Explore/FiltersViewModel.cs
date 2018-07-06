using AppCRM.Extensions;
using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfAutoComplete.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Explore
{
    public class FiltersViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateExploreService _candidateExploreService;
        private readonly IDDLService _iDDLService;

        private ObservableCollection<object> _jobTypeCollection;
        private ObservableCollection<object> _jobTypeSelected;
        private ObservableCollection<object> _categoryCollection;
        private ObservableCollection<object> _categorySelected;
        private ObservableCollection<object> _locationCollection;
        private ObservableCollection<object> _locationSelected;
        private ObservableCollection<object> _positionCollection;
        private ObservableCollection<object> _positionSelected;
        private ObservableCollection<object> _skillCollection;
        private ObservableCollection<object> _skillSelected;
        private ObservableCollection<object> _qualificationCollection;
        private ObservableCollection<object> _qualificationSelected;
        private ObservableCollection<object> _licenceCollection;
        private ObservableCollection<object> _licenceSelected;
        private InitFilter _initDataFilter;

        private bool _isJobTypeEditing;
        private bool _isCategoryEditing;
        private bool _isLocationEditing;
        private bool _isPositionEditing;
        private bool _isSkillEditing;
        private bool _isQualificationEditing;
        private bool _isLicenceEditing;

        public InitFilter InitDataFilter
        {
            get
            {
                return _initDataFilter;
            }
            set
            {
                _initDataFilter = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> JobTypeCollection
        {
            get
            {
                return _jobTypeCollection;
            }
            set
            {
                _jobTypeCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> JobTypeSelected
        {
            get
            {
                return _jobTypeSelected;
            }
            set
            {
                _jobTypeSelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> CategoryCollection
        {
            get
            {
                return _categoryCollection;
            }
            set
            {
                _categoryCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> CategorySelected
        {
            get
            {
                return _categorySelected;
            }
            set
            {
                _categorySelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> LocationCollection
        {
            get
            {
                return _locationCollection;
            }
            set
            {
                _locationCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> LocationSelected
        {
            get
            {
                return _locationSelected;
            }
            set
            {
                _locationSelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> PositionCollection
        {
            get
            {
                return _positionCollection;
            }
            set
            {
                _positionCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> PositionSelected
        {
            get
            {
                return _positionSelected;
            }
            set
            {
                _positionSelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> SkillCollection
        {
            get
            {
                return _skillCollection;
            }
            set
            {
                _skillCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> SkillSelected
        {
            get
            {
                return _skillSelected;
            }
            set
            {
                _skillSelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> QualificationCollection
        {
            get
            {
                return _qualificationCollection;
            }
            set
            {
                _qualificationCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> QualificationSelected
        {
            get
            {
                return _qualificationSelected;
            }
            set
            {
                _qualificationSelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> LicenceCollection
        {
            get
            {
                return _licenceCollection;
            }
            set
            {
                _licenceCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> LicenceSelected
        {
            get
            {
                return _licenceSelected;
            }
            set
            {
                _licenceSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsJobTypeEditing
        {
            get
            {
                return _isJobTypeEditing;
            }
            set
            {
                _isJobTypeEditing = value;
                OnPropertyChanged();
            }
        }
        public bool IsCategoryEditing
        {
            get
            {
                return _isCategoryEditing;
            }
            set
            {
                _isCategoryEditing = value;
                OnPropertyChanged();
            }
        }
        public bool IsLocationEditing
        {
            get
            {
                return _isLocationEditing;
            }
            set
            {
                _isLocationEditing = value;
                OnPropertyChanged();
            }
        }
        public bool IsPositionEditing
        {
            get
            {
                return _isPositionEditing;
            }
            set
            {
                _isPositionEditing = value;
                OnPropertyChanged();
            }
        }
        public bool IsSkillEditing
        {
            get
            {
                return _isSkillEditing;
            }
            set
            {
                _isSkillEditing = value;
                OnPropertyChanged();
            }
        }
        public bool IsQualificationEditing
        {
            get
            {
                return _isQualificationEditing;
            }
            set
            {
                _isQualificationEditing = value;
                OnPropertyChanged();
            }
        }
        public bool IsLicenceEditing
        {
            get
            {
                return _isLicenceEditing;
            }
            set
            {
                _isLicenceEditing = value;
                OnPropertyChanged();
            }
        }

        public FiltersViewModel(IDialogService dialogService, ICandidateExploreService candidateExploreService, IDDLService iDDLService)
        {
            _dialogService = dialogService;
            _candidateExploreService = candidateExploreService;
            _iDDLService = iDDLService;
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackCommandAsync);
        public ICommand BtnSaveProfileCommand => new AsyncCommand(BtnSaveProfileCommandAsync);
        public ICommand AutoCompleteChangedCommand => new Command(() => { });
        public ICommand JobTypeTappedCommand => new Command(() => { IsJobTypeEditing = !IsJobTypeEditing; });
        public ICommand CategoryTappedCommand => new Command(() => { IsCategoryEditing = !IsCategoryEditing; });
        public ICommand LocationTappedCommand => new Command(() => { IsLocationEditing = !IsLocationEditing; });
        public ICommand PositionTappedCommand => new Command(() => { IsPositionEditing = !IsPositionEditing; });
        public ICommand SkillTappedCommand => new Command(() => { IsSkillEditing = !IsSkillEditing; });
        public ICommand QualificationTappedCommand => new Command(() => { IsQualificationEditing = !IsQualificationEditing; });
        public ICommand LicenceTappedCommand => new Command(() => { IsLicenceEditing = !IsLicenceEditing; });

        public override async Task InitializeAsync(object navigationData)
        {
            var pop = await _dialogService.OpenLoadingPopup();

            InitDataFilter = await _iDDLService.GetInitDataFilter();
            JobTypeCollection = InitDataFilter.JobTypeDLL.Cast<object>().ToObservableCollection();
            CategoryCollection = InitDataFilter.ClassificationDLL.Cast<object>().ToObservableCollection();
            LocationCollection = InitDataFilter.LocationDDL.Cast<object>().ToObservableCollection();
            PositionCollection = InitDataFilter.PositionDLL.Cast<object>().ToObservableCollection();
            SkillCollection = InitDataFilter.SkillsDLL.Cast<object>().ToObservableCollection();
            QualificationCollection = InitDataFilter.QualificationDLL.Cast<object>().ToObservableCollection();
            LicenceCollection = InitDataFilter.TicketsDLL.Cast<object>().ToObservableCollection();

            var objSearchDifinition = await _candidateExploreService.GetSavedSearchDefinition();
            if (objSearchDifinition["parameter"] != null)
            {
                string[] jobTypeIds = objSearchDifinition["parameter"]["JobTypeIds"].ToString().Split(',');
                JobTypeSelected = new ObservableCollection<object>(JobTypeCollection.Where(x => Array.IndexOf(jobTypeIds, (x as LookupItem).Id) >= 0));

                string[] categoryIds = objSearchDifinition["parameter"]["CategoryIds"].ToString().Split(',');
                CategorySelected = new ObservableCollection<object>(CategoryCollection.Where(x => Array.IndexOf(categoryIds, (x as LookupItem).Id) >= 0));

                string[] locationIds = objSearchDifinition["parameter"]["LocationIds"].ToString().Split(',');
                LocationSelected = new ObservableCollection<object>(LocationCollection.Where(x => Array.IndexOf(locationIds, (x as LookupItem).Id) >= 0));

                string[] positionIds = objSearchDifinition["parameter"]["PositionIds"].ToString().Split(',');
                PositionSelected = new ObservableCollection<object>(PositionCollection.Where(x => Array.IndexOf(positionIds, (x as LookupItem).Id) >= 0));

                string[] skillIds = objSearchDifinition["parameter"]["SkillsIds"].ToString().Split(',');
                SkillSelected = new ObservableCollection<object>(SkillCollection.Where(x => Array.IndexOf(skillIds, (x as LookupItem).Id) >= 0));

                string[] qualificationIds = objSearchDifinition["parameter"]["QualificationsIds"].ToString().Split(',');
                QualificationSelected = new ObservableCollection<object>(QualificationCollection.Where(x => Array.IndexOf(qualificationIds, (x as LookupItem).Id) >= 0));

                string[] licenceIds = objSearchDifinition["parameter"]["TicketLicensesIds"].ToString().Split(',');
                LicenceSelected = new ObservableCollection<object>(LicenceCollection.Where(x => Array.IndexOf(licenceIds, (x as LookupItem).Id) >= 0));
            }

            await _dialogService.CloseLoadingPopup(pop);
        }
        private async Task BtnSaveProfileCommandAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            SearchParameters parameter = new SearchParameters
            {
                JobTypeIds = string.Join(",", JobTypeSelected.Cast<LookupItem>().Select(r => r.Id.ToString())),
                //SalaryEstimateIds = _fieldSalaryEstimateIds,
                CategoryIds = string.Join(",", CategorySelected.Cast<LookupItem>().Select(r => r.Id.ToString())),
                LocationIds = string.Join(",", LocationSelected.Cast<LookupItem>().Select(r => r.Id.ToString())),
                PositionIds = string.Join(",", PositionSelected.Cast<LookupItem>().Select(r => r.Id.ToString())),
                SkillsIds = string.Join(",", SkillSelected.Cast<LookupItem>().Select(r => r.Id.ToString())),
                QualificationsIds = string.Join(",", QualificationSelected.Cast<LookupItem>().Select(r => r.Id.ToString())),
                TicketLicensesIds = string.Join(",", LicenceSelected.Cast<LookupItem>().Select(r => r.Id.ToString()))
            };
            dynamic obj = await _candidateExploreService.SaveSearchDefinition(parameter);

            try
            {
                if (obj["Success"] == "true")
                {
                    (CandidateMainViewModel.Current.ExplorePage as CandidateExploreViewModel).FilterParameters = parameter;
                    await _dialogService.PopupMessage("Save Search Difinition Successefully", "#52CD9F", "#FFFFFF");
                    await PopupNavigation.Instance.PopAllAsync();
                }
                else
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

        private async Task BtnBackCommandAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
