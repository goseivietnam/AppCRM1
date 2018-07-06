using AppCRM.Extensions;
using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
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

        private ObservableCollection<PickerItem> _jobTypeCollection;
        private ObservableCollection<PickerItem> _jobTypeSelected;
        private ObservableCollection<PickerItem> _categoryCollection;
        private ObservableCollection<PickerItem> _categorySelected;
        private ObservableCollection<PickerItem> _locationCollection;
        private ObservableCollection<PickerItem> _locationSelected;
        private ObservableCollection<PickerItem> _positionCollection;
        private ObservableCollection<PickerItem> _positionSelected;
        private ObservableCollection<PickerItem> _skillCollection;
        private ObservableCollection<PickerItem> _skillSelected;
        private ObservableCollection<PickerItem> _qualificationCollection;
        private ObservableCollection<PickerItem> _qualificationSelected;
        private ObservableCollection<PickerItem> _licenceCollection;
        private ObservableCollection<PickerItem> _licenceSelected;
        private InitFilter _initDataFilter;

        private string _fieldJobType;
        private string _fieldCategory;
        private string _fieldLocation;
        private string _fieldPosition;
        private string _fieldSkill;
        private string _fieldQualification;
        private string _fieldLicence;

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
        public ObservableCollection<PickerItem> JobTypeCollection
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
        public ObservableCollection<PickerItem> JobTypeSelected
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
        public ObservableCollection<PickerItem> CategoryCollection
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
        public ObservableCollection<PickerItem> CategorySelected
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
        public ObservableCollection<PickerItem> LocationCollection
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
        public ObservableCollection<PickerItem> LocationSelected
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
        public ObservableCollection<PickerItem> PositionCollection
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
        public ObservableCollection<PickerItem> PositionSelected
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
        public ObservableCollection<PickerItem> SkillCollection
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
        public ObservableCollection<PickerItem> SkillSelected
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
        public ObservableCollection<PickerItem> QualificationCollection
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
        public ObservableCollection<PickerItem> QualificationSelected
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
        public ObservableCollection<PickerItem> LicenceCollection
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
        public ObservableCollection<PickerItem> LicenceSelected
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

        public FiltersViewModel(IDialogService dialogService, ICandidateExploreService candidateExploreService, IDDLService iDDLService)
        {
            _dialogService = dialogService;
            _candidateExploreService = candidateExploreService;
            _iDDLService = iDDLService;
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackCommandAsync);
        public ICommand BtnSaveProfileCommand => new AsyncCommand(BtnSaveProfileCommandAsync);
        public ICommand JobTypeChangeCommand => new Command(UpdateJobType);
        public ICommand CategoryChangeCommand => new Command(UpdateCategory);
        public ICommand LocationChangeCommand => new Command(UpdateLocation);
        public ICommand PositionChangeCommand => new Command(UpdatePosition);
        public ICommand SkillChangeCommand => new Command(UpdateSkill);
        public ICommand QualificationChangeCommand => new Command(UpdateQualification);
        public ICommand LicenceChangeCommand => new Command(UpdateLicence);

        private void UpdateJobType(object selectedValues)
        {
           // _fieldJobType = String.Join(",", (selectedValues as List<string>).ToArray());
        }
        private void UpdateCategory(object selectedValues)
        {
            _fieldCategory = String.Join(",", (selectedValues as List<string>).ToArray());
        }
        private void UpdateLocation(object selectedValues)
        {
            _fieldLocation = String.Join(",", (selectedValues as List<string>).ToArray());
        }
        private void UpdatePosition(object selectedValues)
        {
            _fieldPosition = String.Join(",", (selectedValues as List<string>).ToArray());
        }
        private void UpdateSkill(object selectedValues)
        {
            _fieldSkill = String.Join(",", (selectedValues as List<string>).ToArray());
        }
        private void UpdateQualification(object selectedValues)
        {
            _fieldQualification = String.Join(",", (selectedValues as List<string>).ToArray());
        }
        private void UpdateLicence(object selectedValues)
        {
            _fieldLicence = String.Join(",", (selectedValues as List<string>).ToArray());
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var pop = await _dialogService.OpenLoadingPopup();

            InitDataFilter = await _iDDLService.GetInitDataFilter();
            JobTypeCollection = InitDataFilter.JobTypeDLL.ToObservableCollection();
            CategoryCollection = InitDataFilter.ClassificationDLL.ToObservableCollection();
            LocationCollection = InitDataFilter.LocationDDL.ToObservableCollection();
            PositionCollection =InitDataFilter.PositionDLL.ToObservableCollection();
            SkillCollection =InitDataFilter.SkillsDLL.ToObservableCollection();
            QualificationCollection = InitDataFilter.QualificationDLL.ToObservableCollection();
            LicenceCollection = InitDataFilter.TicketsDLL.ToObservableCollection();

            var objSearchDifinition = await _candidateExploreService.GetSavedSearchDefinition();
            if (objSearchDifinition["parameter"] != null)
            {
                string JobTypeIds = objSearchDifinition["parameter"]["JobTypeIds"].ToString();
                List<Guid> JobTypeIdsList = new List<Guid>();
                foreach (var id in JobTypeIds.Split(','))
                {
                    JobTypeIdsList.Add(new Guid(id));
                }

                JobTypeSelected = JobTypeCollection.Where(x => JobTypeIdsList.Contains(x.Id)).ToObservableCollection();
            }

            await _dialogService.CloseLoadingPopup(pop);
        }
        private async Task BtnSaveProfileCommandAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            SearchParameters parameter = new SearchParameters
            {
                JobTypeIds = _fieldJobType,
                //SalaryEstimateIds = _fieldSalaryEstimateIds,
                CategoryIds = _fieldCategory,
                LocationIds = _fieldLocation,
                PositionIds = _fieldPosition,
                SkillsIds = _fieldSkill,
                QualificationsIds = _fieldQualification,
                TicketLicensesIds = _fieldLicence
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
