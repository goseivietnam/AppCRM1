using AppCRM.Controls;
using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Profile
{
    class AddWorkExprienceViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateDetailsService _candidateDetailsService;
        private readonly INavigationService _navigationService;

        private string _title;
        private string _company;
        private string _location;
        private bool _isWorkCurrent;
        private DateTime? _fromDate;
        private DateTime? _toDate;
        private bool _btnAttachmentIsEnable = true;
        private string _fileName;
        private bool _fileNameIsVisible = false;
        private bool _fileAttachImageIsVisible = false;
        private SJFileStream stream;

        public AddWorkExprienceViewModel(IDialogService dialogService, ICandidateDetailsService candidateDetailsService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateDetailsService = candidateDetailsService;
            _navigationService = navigationService;
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Company
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
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }
        public bool IsWorkCurrent
        {
            get
            {
                return _isWorkCurrent;
            }
            set
            {
                _isWorkCurrent = value;
                OnPropertyChanged();
            }
        }
        public DateTime? FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime? ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
                OnPropertyChanged();
            }
        }
        public bool BtnAttachmentIsEnable
        {
            get
            {
                return _btnAttachmentIsEnable;
            }
            set
            {
                _btnAttachmentIsEnable = value;
                OnPropertyChanged();
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }
        public bool FileNameIsVisible
        {
            get
            {
                return _fileNameIsVisible;
            }
            set
            {
                _fileNameIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool FileAttachImageIsVisible
        {
            get
            {
                return _fileAttachImageIsVisible;
            }
            set
            {
                _fileAttachImageIsVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand BtnBackCommand => new AsyncCommand(BtnBackAsync);
        public ICommand BtnSaveWorkExprienceCommand => new AsyncCommand(BtnSaveWorkExprienceCommandAsync);
        public ICommand BtnAttachmentCommand => new AsyncCommand(BtnAttachmentAsync);

        private async Task BtnBackAsync()
        {
            var result = await _dialogService.Alert("You have unsaved change", "Do you want discard your changes?", "Discard", "Cancel");
            if (result)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private async Task BtnSaveWorkExprienceCommandAsync()
        {
            IsBusy = true;
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            try
            {
                dateFrom = _fromDate;
            }
            catch
            {
                dateFrom = null;
            }
            try
            {
                dateTo = _toDate;
            }
            catch
            {
                dateTo = null;
            }
            ContactWorkExprience work = new ContactWorkExprience
            {
                Title = _title,
                Company = _title,
                Location = _location,
                IsWorkCurrent = _isWorkCurrent,
                From = dateFrom,
                TimeFromString = dateFrom.HasValue ? dateFrom.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                To = dateTo,
                TimeToString = dateTo.HasValue ? dateTo.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
            };
            var obj = await _candidateDetailsService.AddWorkExprience(work);
            IsBusy = false;

            if (obj != null)
            {
                try
                {
                    if (obj["Success"] == "true") //success
                    {
                        await _dialogService.PopupMessage("Add new Work Exprience Successefully", "#52CD9F", "#FFFFFF");
                        IsBusy = true;
                        var objupload = await _candidateDetailsService.SaveWorkExperienceAttachment(obj["Result"], stream);
                        IsBusy = false;

                        if (objupload != null)
                        {
                            try
                            {
                                if (objupload["Success"] == "true") //success
                                {
                                    await _dialogService.PopupMessage("Attach file Successefully", "#52CD9F", "#FFFFFF");
                                    await PopupNavigation.Instance.PopAllAsync();
                                    await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                                }
                                else if (objupload["Success"] == "false")
                                {
                                    if (objupload["Message"] == "Fail")
                                    {
                                        await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                                    }
                                    else if (objupload["Message"] == "NodocumentFile")
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
                catch
                {
                    await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                    IsBusy = false;
                }
            }
        }

        private async Task BtnAttachmentAsync()
        {
            BtnAttachmentIsEnable = false;
            stream = await DependencyService.Get<IFilePicker>().GetFileStreamAsync(Tools.Enum.FileTypeDocAndPdf);
            BtnAttachmentIsEnable = true;
            FileName = stream.FileName;
            FileNameIsVisible = true;
            FileAttachImageIsVisible = true;
        }
    }
}
