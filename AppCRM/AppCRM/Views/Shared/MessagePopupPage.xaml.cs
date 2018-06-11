using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppCRM.Views.Shared
{
	public partial class MessagePopupPage : PopupPage
	{
        public string Message { get; set; }
        public string LayoutColor { get; set; } = "#ffffff";
        public string MessageColor { get; set; } = "#000000";

        public MessagePopupPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            messageLayout.BackgroundColor = Color.FromHex(LayoutColor);
            messageText.Text = Message;
            messageText.TextColor = Color.FromHex(MessageColor);
            HidePopup();
        }

        private async void HidePopup()
        {
            await Task.Delay(4000);
            await PopupNavigation.Instance.RemovePageAsync(this);
        }
    }
}