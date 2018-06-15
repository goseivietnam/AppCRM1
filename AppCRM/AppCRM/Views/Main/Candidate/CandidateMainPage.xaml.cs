using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Navigation;
using AppCRM.ViewModels.Base;
using AppCRM.ViewModels.Main.Candidate;
using AppCRM.Views.Main.Candidate.ProfilePage;
using Naxam.Controls.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCRM.Views.Main.Candidate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CandidateMainPage : MasterDetailPage
	{
        private readonly INavigationService _navigationService;
        private readonly ICandidateDetailsService _candidateDetailService;
        public CandidateMainPage ()
		{
			InitializeComponent ();

             _navigationService = Locator.Instance.Resolve<INavigationService>();
            _candidateDetailService  = Locator.Instance.Resolve<ICandidateDetailsService>();
            Master = new CandidateMaster();

            var tabs = new BottomTabbedPage();

            tabs.Children.Add(new CandidateProfilePage());

            Detail = tabs;
            Detail.BindingContext = Locator.Instance.Resolve<CandidateProfileViewModel>() as ViewModelBase; ;
            (Detail.BindingContext as ViewModelBase).InitializeAsync(tabs);
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            
        }
        //event click item in menu 
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItem;
            if (item != null)
            {
                _navigationService.NavigateToAsync(item.TargetType);
                MasterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}