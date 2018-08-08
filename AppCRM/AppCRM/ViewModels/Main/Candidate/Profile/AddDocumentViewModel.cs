using AppCRM.Controls;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Services.Navigation;
using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Main.Candidate.Profile
{
    public class AddDocumentViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICandidateDetailsService _candidateDetailsService;
        private readonly INavigationService _navigationService;

        private string _title;
        private bool _btnAttachmentIsEnable = true;
        private string _fileName;
        private bool _fileNameIsVisible = false;
        private bool _fileAttachImageIsVisible = false;
        private SJFileStream stream;

        public AddDocumentViewModel(IDialogService dialogService, ICandidateDetailsService candidateDetailsService, INavigationService navigationService)
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
        public ICommand BtnSaveDocumentCommand => new AsyncCommand(BtnSaveDocumentCommandAsync);
        public ICommand BtnAttachmentCommand => new AsyncCommand(BtnAttachmentAsync);

        private async Task BtnBackAsync()
        {
            var result = await _dialogService.Alert("You have unsaved change", "Do you want discard your changes?", "Discard", "Cancel");
            if (result)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private async Task BtnSaveDocumentCommandAsync()
        {
            var pop = await _dialogService.OpenLoadingPopup();
            string fileName = _title + Path.GetExtension(stream.FileName);
            Dictionary<string, object> obj = await _candidateDetailsService.AddDocument(stream, fileName);
            await _dialogService.CloseLoadingPopup(pop);

            if (obj != null)
            {
                try
                {
                    if (obj["Success"].ToString() == "true") //success
                    {
                        await _dialogService.PopupMessage("Add new Document Successefully", "#52CD9F", "#FFFFFF");
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
