﻿using AppCRM.Controls;
using AppCRM.Extensions;
using AppCRM.Models;
using AppCRM.Services.Authentication;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Services.Request;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Account
{
    public class CandidateRegisterViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly ICandidateDetailsService _candidateDetailsService;
        private readonly INavigationService _navigationService;

        private string _fieldFirstName;
        private string _fieldLastName;
        private string _fieldEmail;
        private string _fieldPassword;
        private string _fieldPasswordConfirm;
        private ImageSource _profileAvatarSource;
        private ObservableCollection<object> _jobInterestCollection;
        private ObservableCollection<object> _jobLocationCollection;
        private ObservableCollection<object> _selectedJobs;
        private ObservableCollection<object> _selectedLocations;

        private SJFileStream _avatarStream;

        public CandidateRegisterViewModel(IAuthenticationService authenticationService, IDialogService dialogService, ICandidateDetailsService candidateDetailsService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _candidateDetailsService = candidateDetailsService;
            _navigationService = navigationService;
        }

        public string FieldFirstName
        {
            get
            {
                return _fieldFirstName;
            }
            set
            {
                _fieldFirstName = value;
                OnPropertyChanged();
            }
        }
        public string FieldLastName
        {
            get
            {
                return _fieldLastName;
            }
            set
            {
                _fieldLastName = value;
                OnPropertyChanged();
            }
        }
        public string FieldEmail
        {
            get
            {
                return _fieldEmail;
            }
            set
            {
                _fieldEmail = value;
                OnPropertyChanged();
            }
        }
        public string FieldPassword
        {
            get
            {
                return _fieldPassword;
            }
            set
            {
                _fieldPassword = value;
                OnPropertyChanged();
            }
        }
        public string FieldPasswordConfirm
        {
            get
            {
                return _fieldPasswordConfirm;
            }
            set
            {
                _fieldPasswordConfirm = value;
                OnPropertyChanged();
            }
        }
        public ImageSource ProfileAvatarSource
        {
            get
            {
                return _profileAvatarSource;
            }
            set
            {
                _profileAvatarSource = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<object> JobInterestCollection
        {
            get { return _jobInterestCollection; }
            set { _jobInterestCollection = value; OnPropertyChanged(); }
        }
        public ObservableCollection<object> JobLocationCollection
        {
            get { return _jobLocationCollection; }
            set { _jobLocationCollection = value; OnPropertyChanged(); }
        }
        public ObservableCollection<object> SelectedJobs
        {
            get { return _selectedJobs; }
            set { _selectedJobs = value; OnPropertyChanged(); }
        }
        public ObservableCollection<object> SelectedLocations
        {
            get { return _selectedLocations; }
            set { _selectedLocations = value; OnPropertyChanged(); }
        }

        public ICommand SubmitRegisterCommand => new AsyncCommand(SubmitRegisterAsync);
        public ICommand BtnCancelCommand => new AsyncCommand(BtnCancelAsync);
        public ICommand PickAvatarBtnCommand => new AsyncCommand(PickAvatar);
        public ICommand SelectionChangedCommand => new Command(SelectionChanged);

        private async Task SubmitRegisterAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            Register reg = new Register
            {
                FirstName = _fieldFirstName,
                LastName = _fieldLastName,
                Email = _fieldEmail,
                Password = _fieldPassword,
                UserName = _fieldEmail,
                ConfirmPassword = _fieldPasswordConfirm,
                InterestedRoleIds = string.Join(",", SelectedJobs.Select(r => (r as LookupItem).Id)),
                InterestedLocationIds = string.Join(",", SelectedLocations.Select(r => (r as LookupItem).Id))
            };

            Dictionary<string, object> obj = await _candidateDetailsService.CandidateRegister(reg);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"].ToString() == "true") //success
                    {
                        if (obj["Roles"].ToString() == "Employer")
                        {
                        }
                        else if (obj["Roles"].ToString() == "Candidate")
                        {
                            await _dialogService.PopupMessage("Register Successefully", "#52CD9F", "#FFFFFF");
                            App.ContactID = obj["ContactID"].ToString();
                            App.UserName = obj["UserName"].ToString();
                            App.PassWord = FieldPassword;
                            RequestService.ACCESS_TOKEN = obj["access_token"].ToString();

                            if (_avatarStream != null) { 
                                Dictionary<string, object> objUpload = await _candidateDetailsService.AddEditContactAvatarImage(_avatarStream);
                            try
                            {
                                    if (objUpload["Success"].ToString() == "true") //success
                                {
                                    await _dialogService.PopupMessage("Update Cover image Successefully", "#52CD9F", "#FFFFFF");
                                }
                                    else if (objUpload["Success"].ToString() == "false")
                                {
                                    await _dialogService.PopupMessage("Haven't image file, please try again!!", "#CF6069", "#FFFFFF");
                                }
                            }
                            catch
                            {
                                await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                                await _dialogService.CloseLoadingPopup(pop);
                            }
                            finally
                            {
                                await PopupNavigation.Instance.PopAllAsync();
                                await NavigationService.NavigateToAsync<LoginViewModel>();
                            }
                        } else {
                                await PopupNavigation.Instance.PopAllAsync();
                                await NavigationService.NavigateToAsync<LoginViewModel>();
                        }
                        }
                    }
                    else if (obj["Message"].ToString() == "IsExists") //is exists
                    {
                        await _dialogService.PopupMessage("This account is exists!", "#CF6069", "#FFFFFF");
                    }
                    else if (obj["Message"].ToString() == "TryAgaint") //fail
                    {
                        await _dialogService.PopupMessage("An error has occurred, please try again!", "#CF6069", "#FFFFFF");
                    }
                }
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!", "#CF6069", "#FFFFFF");
                    await _dialogService.CloseLoadingPopup(pop);
                }
            }

            await _dialogService.CloseLoadingPopup(pop);
        }
        private async Task BtnCancelAsync()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        private async Task PickAvatar()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            var stream = await DependencyService.Get<IFilePicker>().GetImageStreamAsync();
            byte[] _avatarStreamBuffer = Tools.Utilities.ReadToEnd(stream.Stream);
            if (stream != null && stream.Stream != null)
            {
                _avatarStream = new SJFileStream { FileName = stream.FileName, Stream = new MemoryStream(_avatarStreamBuffer) };
                ProfileAvatarSource = ImageSource.FromStream(() => new MemoryStream(_avatarStreamBuffer));
            }
            await _dialogService.CloseLoadingPopup(pop);
        }
        private void SelectionChanged(object obj)
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            var InterestedRolesDDL = await _candidateDetailsService.GetInterestedRolesDDL();
            JobInterestCollection = InterestedRolesDDL.Cast<object>().ToObservableCollection();
            SelectedJobs = new ObservableCollection<object>();

            var InterestedLocationsDDL = await _candidateDetailsService.GetInterestedLocationsDDL();
            JobLocationCollection = InterestedLocationsDDL.Cast<object>().ToObservableCollection();
            SelectedLocations = new ObservableCollection<object>();
        }

    }
}
