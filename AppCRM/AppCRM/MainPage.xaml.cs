using AppCRM.Views.Shared;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace AppCRM
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
         //   DialogInit();
        }

        protected async void DialogInit()
        {
            await App.Current.MainPage.Navigation.PushPopupAsync(new MessagePopupPage() { Message = "hello", LayoutColor = "#52CD9F", MessageColor = "#FFFFFF" });
        }
    }
}
