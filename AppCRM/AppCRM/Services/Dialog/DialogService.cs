﻿using AppCRM.ViewModels;
using AppCRM.ViewModels.Base;
using AppCRM.Views.Shared;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace AppCRM.Services.Dialog
{
    public interface IDialogService
    {
        Task PopupMessage(string message, string color, string textColor);
        
        Task<bool> Alert(string title, string message, string accept, string cancel);

        Task Alert(string title, string message, string cancel);

        void CloseAllPopup();
        Task<PopupPage> OpenLoadingPopup();
        Task CloseLoadingPopup(PopupPage _loadingPopup);
    }
    public class DialogService :IDialogService
    {
        public async Task PopupMessage(string message, string color, string textColor)
        {
            await PopupNavigation.Instance.PushAsync(new MessagePopupPage() { Message = message, LayoutColor = color, MessageColor = textColor });
        }

        public async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        public async Task<bool> Alert(string title, string message, string accept, string cancel)
        {
            return await App.Current.MainPage.DisplayAlert(title,message,accept,cancel);
        }

        public async Task Alert(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<PopupPage> OpenLoadingPopup()
        {
            LoadingPopupPage _loadingPopup = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(_loadingPopup,true);
            return _loadingPopup;
        }

        public async Task CloseLoadingPopup(PopupPage _loadingPopup)
        {
            await PopupNavigation.Instance.RemovePageAsync(_loadingPopup, true);
        }
    }
}
