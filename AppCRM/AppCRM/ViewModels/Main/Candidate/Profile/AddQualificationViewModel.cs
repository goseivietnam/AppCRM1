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
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Profile
{
    public class AddQualificationViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateDetailsService _candidateDetailsService;
        private readonly INavigationService _navigationService;

        private string _qualification;
        private string _institution;
        private DateTime? _fromDate;
        private DateTime? _toDate;
        private bool _btnAttachmentIsEnable = true;
        private string _fileName;
        private bool _fileNameIsVisible = false;
        private bool _fileAttachImageIsVisible = false;
        private SJFileStream stream;

        public AddQualificationViewModel(IDialogService dialogService, ICandidateDetailsService candidateDetailsService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _candidateDetailsService = candidateDetailsService;
            _navigationService = navigationService;
        }

        public string Qualification
        {
            get
            {
                return _qualification;
            }
            set
            {
                _qualification = value;
                OnPropertyChanged();
            }
        }

        public string Institution
        {
            get
            {
                return _institution;
            }
            set
            {
                _institution = value;
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
        public ICommand BtnSaveQualificationCommand => new AsyncCommand(BtnSaveQualificationCommandAsync);
        public ICommand BtnAttachmentCommand => new AsyncCommand(BtnAttachmentAsync);

        private async Task BtnBackAsync()
        {
            var result = await _dialogService.Alert("You have unsaved change", "Do you want discard your changes?", "Discard", "Cancel");
            if (result)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private async Task BtnSaveQualificationCommandAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
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
            ContactQualification qualification = new ContactQualification
            {
                MeasurementName = _qualification,
                Institute = _institution,
                From = dateFrom,
                DateFromString = dateFrom.HasValue ? dateFrom.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                To = dateTo,
                DateToString = dateTo.HasValue ? dateTo.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
            };
            Dictionary<string, object> obj = await _candidateDetailsService.AddQualification(qualification);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"].ToString() == "true") //success
                    {
                        await _dialogService.PopupMessage("Add new Qualification Successefully", "#52CD9F", "#FFFFFF");
                        if (stream != null)
                        {
                            Dictionary<string, object> objupload = await _candidateDetailsService.SaveContactQualificationAttachment(obj["Result"].ToString(), stream);

                            if (objupload != null)
                            {
                                try
                                {
                                    if (objupload["Success"].ToString() == "true") //success
                                    {
                                        await _dialogService.PopupMessage("Attach file Successefully", "#52CD9F", "#FFFFFF");
                                        await PopupNavigation.Instance.PopAllAsync();
                                        await _navigationService.NavigateToAsync<CandidateMainViewModel>();
                                    }
                                    else if (objupload["Success"].ToString() == "false")
                                    {
                                        if (objupload["Message"].ToString() == "Fail")
                                        {
                                            await _dialogService.PopupMessage("An error has occurred, please try again!!", "#CF6069", "#FFFFFF");
                                        }
                                        else if (objupload["Message"].ToString() == "NodocumentFile")
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
                        }
                        else
                        {
                            await PopupNavigation.Instance.PopAllAsync();
                            await _navigationService.NavigateToAsync<CandidateMainViewModel>();
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
