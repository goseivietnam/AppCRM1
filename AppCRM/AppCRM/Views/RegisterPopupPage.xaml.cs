using AppCRM.Views.Account;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace AppCRM.Views
{
	[XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class RegisterPopupPage : PopupPage
    {
		public RegisterPopupPage ()
		{
			InitializeComponent ();
		}
        protected override bool OnBackButtonPressed()
        {
            CloseAllPopup();
            return false;
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
      
    }
}