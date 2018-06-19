using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Navigation;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate;
using AppCRM.Views.Main.Candidate.ProfilePage;
using Syncfusion.XForms.TabView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCRM.Views.Main.Candidate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CandidateMainPage : MasterDetailPage
	{
        public CandidateMainPage ()
		{
			InitializeComponent ();
        }
    }
}