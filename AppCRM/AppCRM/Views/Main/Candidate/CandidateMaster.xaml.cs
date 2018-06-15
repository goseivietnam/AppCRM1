using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCRM.Views.Main.Candidate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CandidateMaster : ContentPage
	{
        public ListView ListView { get { return listView; } }
        public CandidateMaster ()
		{
			InitializeComponent ();
		}
	}
}