using AppCRM.Views.Shared;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCRM.Services.Dialog
{
    public interface IDialogService
    {
        Task PopupMessage(string message, string color, string textColor);
    }
    public class DialogService :IDialogService
    {
        public async Task PopupMessage(string message, string color, string textColor)
        {
            await PopupNavigation.Instance.PushAsync(new MessagePopupPage() { Message = message, LayoutColor = color, MessageColor = textColor });
        }
    }
}
