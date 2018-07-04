using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Explore
{
    public class JobDetailViewModel : ViewModelBase
    {
        private readonly ICandidateExploreService _candidateExploreService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Guid? _vacancyID;

        private Vacancy _job;

        public JobDetailViewModel(IDialogService dialogService, ICandidateExploreService candidateExploreService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateExploreService = candidateExploreService;
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
            
            await _dialogService.CloseLoadingPopup(pop);
        }
    }
}
