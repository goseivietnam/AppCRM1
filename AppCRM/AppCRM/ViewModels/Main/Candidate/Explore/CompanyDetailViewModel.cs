using AppCRM.Models;
using AppCRM.Services.Candidate;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Explore
{
    public class CompanyDetailViewModel : ViewModelBase
    {
        private readonly ICandidateExploreService _candidateExploreService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Guid? _accountID;

        private Models.Account _company;
        private List<Vacancy> _vacancies;

        public CompanyDetailViewModel(IDialogService dialogService, ICandidateExploreService candidateExploreService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateExploreService = candidateExploreService;
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
            _accountID = (Guid)navigationData;

            Company = new Models.Account
            {
                AboutUs = "The first is a non technical method which requires the use of adware removal software. Download free adware and spyware removal software and use advanced tools to help prevent getting infected.Spyware scan review is a free service for anyone interested in downloading spyware / adware removal software.Our adware remover is the most trusted adware removal software in the world. Additionally, adware operations are increasingly asking that their software no longer be uninstalled by adware / spyware removal companies.",
                AccountName = "National University of Singapore",
                WebSite = "www.nus.edu.sg",
                Address = "Singapore",
                VideoLink = ""
            };

            Vacancies = new List<Vacancy>
            {
                new Vacancy { Title = "Program Manager", JobType = "Casual", Salary = "18619", Country = "Washington" }
            };

            await _dialogService.CloseLoadingPopup(pop);
        }
    }
}
