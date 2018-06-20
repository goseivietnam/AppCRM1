using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace AppCRM.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPopupPage : PopupPage
	{
		public LoadingPopupPage ()
		{
			InitializeComponent ();
		}
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}