using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels.Main.Candidate.Profile
{
    class AddSkillPageViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateDetailsService _candidateDetailsService;
        private readonly INavigationService _navigationService;

        private string _skill;
        private string _experienceName;
        private Guid? _experienceID;

        public AddSkillPageViewModel(IDialogService dialogService, ICandidateDetailsService candidateDetailsService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateDetailsService = candidateDetailsService;
            _navigationService = navigationService;
        }

        public string Skill
        {
            get
            {
                return _skill;
            }
            set
            {
                _skill = value;
                OnPropertyChanged();
            }
        }

        public string ExperienceName
        {
            get
            {
                return _experienceName;
            }
            set
            {
                _experienceName = value;
                OnPropertyChanged();
            }
        }

        public Guid? ExperienceID
        {
            get
            {
                return _experienceID;
            }
            set
            {
                _experienceID = value;
                OnPropertyChanged();
            }
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand BtnSaveSkillCommand => new AsyncCommand(BtnSaveSkillCommandAsync);

        private async Task BtnBackAsync()
        {
            var result = await _dialogService.Alert("You have unsaved change", "Do you want discard your changes?", "Discard", "Cancel");
            if (result)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private async Task BtnSaveSkillCommandAsync()
        {
            IsBusy = true;
            //PickerItem selecteditem = (PickerItem)Experience.SelectedItem;

            ContactSkill skill = new ContactSkill
            {
                MeasurementName = _skill,
                ExperienceName = _experienceName,
                ExperienceID = _experienceID,
            };
            var obj = await _candidateDetailsService.AddSkill(skill);
            IsBusy = false;
            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _dialogService.PopupMessage("Add new Education Successefully", "#52CD9F", "#FFFFFF");
                        await PopupNavigation.Instance.PopAllAsync();
                        await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                    }
                    else if (obj["Success"] == "false")
                    {
                        if (obj["Message"] == "Fail")
                        {
                            await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                        }
                        else if (obj["Message"] == "AttachFail")
                        {
                            await _dialogService.PopupMessage("Attach file Fail, please try again!!", "#CF6069", "#FFFFFF");
                        }
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                    IsBusy = false;
                }
            }
        }
    }
}
