using AppCRM.ViewModels;
using AppCRM.ViewModels.Base;
using AppCRM.Views.Shared;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace AppCRM.Services.Dialog
{
    public interface IDialogService
    {
        Task PopupMessage(string message, string color, string textColor);
        void CloseAllPopup();
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
    }
}
