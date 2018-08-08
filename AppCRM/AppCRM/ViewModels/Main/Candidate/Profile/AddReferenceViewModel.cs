using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels.Main.Candidate.Profile
{
    class AddReferenceViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateDetailsService _candidateDetailsService;
        private readonly INavigationService _navigationService;

        private string _employer;
        private string _position;
        private string _referenceName;
        private string _relationship;
        private string _phone;
        private bool _isContacted;

        public AddReferenceViewModel(IDialogService dialogService, ICandidateDetailsService candidateDetailsService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateDetailsService = candidateDetailsService;
            _navigationService = navigationService;
        }

        public string Employer
        {
            get
            {
                return _employer;
            }
            set
            {
                _employer = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        public string ReferenceName
        {
            get
            {
                return _referenceName;
            }
            set
            {
                _referenceName = value;
                OnPropertyChanged();
            }
        }

        public string Relationship
        {
            get
            {
                return _relationship;
            }
            set
            {
                _relationship = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public bool IsContacted
        {
            get
            {
                return _isContacted;
            }
            set
            {
                _isContacted = value;
                OnPropertyChanged();
            }
        }
       
        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand BtnSaveReferenceCommand => new AsyncCommand(BtnSaveReferenceCommandAsync);

        private async Task BtnBackAsync()
        {
            var result = await _dialogService.Alert("You have unsaved change", "Do you want discard your changes?", "Discard", "Cancel");
            if (result)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private async Task BtnSaveReferenceCommandAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            ContactReference reference = new ContactReference
            {
                EmployerName = _employer,
                Position = _position,
                ReferenceName = _referenceName,
                Relationship = _relationship,
                Phone = _phone,
                Contacted = _isContacted,
            };
            Dictionary<string, object> obj = await _candidateDetailsService.AddReference(reference);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"].ToString() == "true") //success
                    {
                        await _dialogService.PopupMessage("Add new Reference Successefully", "#52CD9F", "#FFFFFF");
                        await PopupNavigation.Instance.PopAllAsync();
                        await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                    }
                    else if (obj["Success"].ToString() == "false")
                    {
                        if (obj["Message"].ToString() == "Fail")
                        {
                            await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                        }
                        else if (obj["Message"].ToString() == "AttachFail")
                        {
                            await _dialogService.PopupMessage("Attach file Fail, please try again!!", "#CF6069", "#FFFFFF");
                        }
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                    await _dialogService.CloseLoadingPopup(pop);
                }
            }
            await _dialogService.CloseLoadingPopup(pop);
        }
    }
}
