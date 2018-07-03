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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Explore
{
    public class FiltersViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateJobService _candidateJobService;
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

        public FiltersViewModel(IDialogService dialogService, ICandidateJobService candidateJobService, IDDLService iDDLService)
        {
            _dialogService = dialogService;
            _candidateJobService = candidateJobService;
            _iDDLService = iDDLService;
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackCommandAsync);
        //public ICommand BtnSaveProfileCommand => new AsyncCommand(BtnSaveProfileCommandAsync);
        public ICommand JobTypeChangeCommand => new Command(UpdateCity);
        //public ICommand CategoryChangeCommand => new Command(UpdateCity);
        //public ICommand LocationChangeCommand => new Command(UpdateCity);
        //public ICommand PositionChangeCommand => new Command(UpdateCity);
        //public ICommand SkillChangeCommand => new Command(UpdateCity);
        //public ICommand QualificationChangeCommand => new Command(UpdateCity);
        //public ICommand LicenceChangeCommand => new Command(UpdateCity);

        private void UpdateCity(object selectedValues)
        {
            //Guid currentID = new Guid(selectedValues.ToString());
            //CitySelected = CityCollection.Where(x => x.Id == currentID).FirstOrDefault();
        }


        public override async Task InitializeAsync(object navigationData)
        {
            var pop = await _dialogService.OpenLoadingPopup();

            var JobTypeDDL = await _iDDLService.GetJobTypeDDL();
            JobTypeCollection = JobTypeDDL.ToObservableCollection();

            var CategoryDDL = await _iDDLService.GetClassificationDDL();
            CategoryCollection = CategoryDDL.ToObservableCollection();

            var LocationDDL = await _iDDLService.GetLocationDDL("");
            LocationCollection = LocationDDL.ToObservableCollection();

            var PositionDDL = await _iDDLService.GetPositionDDL("");
            PositionCollection = PositionDDL.ToObservableCollection();

            var SkillDDL = await _iDDLService.GetSkillsDDL("");
            SkillCollection = SkillDDL.ToObservableCollection();

            var QualificationDDL = await _iDDLService.GetQualificationDDL();
            QualificationCollection = QualificationDDL.ToObservableCollection();

            var LicenceDDL = await _iDDLService.GetLicenceDDL();
            LicenceCollection = LicenceDDL.ToObservableCollection();

            await _dialogService.CloseLoadingPopup(pop);
        }
        private async Task BtnSaveProfileCommandAsync()
        {

        }

        private async Task BtnBackCommandAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
