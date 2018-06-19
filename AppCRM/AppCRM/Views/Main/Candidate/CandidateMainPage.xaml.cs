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
        //private readonly INavigationService _navigationService;
        //private readonly ICandidateDetailsService _candidateDetailService;

        public CandidateProfilePage CandidateProfilePage;

        public CandidateMainPage ()
		{
			InitializeComponent ();

            // _navigationService = Locator.Instance.Resolve<INavigationService>();
            //_candidateDetailService  = Locator.Instance.Resolve<ICandidateDetailsService>();

            ////Initializing CandidateProfilePage
            //CandidateProfilePage = new CandidateProfilePage();
            //CandidateProfilePage.BindingContext = Locator.Instance.Resolve<CandidateProfileViewModel>() as ViewModelBase;
            //(CandidateProfilePage.BindingContext as ViewModelBase).InitializeAsync(CandidateProfilePage);

            //var tab = new SfTabView();
            //var tabItems = new TabItemCollection
            //{
            //new SfTabItem()
            //{
            //    Title = "Profile",
            //    FontIconFontFamily = "sjTab.ttf",
            //    TitleFontColor = Color.LightGray,
            //    IconFont = "",
            //    Content = CandidateProfilePage,
            //    FontIconFontSize = 24,
            //    TitleFontSize = 10
            //},
            //    new SfTabItem()
            //    {
            //        Title = "Job",
            //        FontIconFontFamily = "sjTab.ttf",
            //        TitleFontColor = Color.LightGray,
            //        IconFont = "",
            //        Content = new ContentView(),
            //        FontIconFontSize = 24,
            //        TitleFontSize = 10
            //    },
            //    new SfTabItem()
            //    {
            //        Title = "Explore",
            //        FontIconFontFamily = "sjTab.ttf",
            //        IconFont = "",
            //        TitleFontColor = Color.LightGray,
            //        Content = new ContentView(),
            //        FontIconFontSize = 24,
            //        TitleFontSize = 10
            //    },
            //    new SfTabItem()
            //    {
            //        Title = "Notify",
            //        FontIconFontFamily = "sjTab.ttf",
            //        IconFont = "",
            //        TitleFontColor = Color.LightGray,
            //        Content = new ContentView(),
            //        FontIconFontSize = 24,
            //        TitleFontSize = 10
            //    },
            //    new SfTabItem()
            //    {
            //        Title = "Message",
            //        FontIconFontFamily = "sjTab.ttf",
            //        IconFont = "",
            //        TitleFontColor = Color.LightGray,
            //        Content = new ContentView(),
            //        FontIconFontSize = 24,
            //        TitleFontSize = 10
            //    },

            //    // profile:  | job:  | explore:  | notify:  | message: 
            //};
            //var selectionIndicatorSettings = new SelectionIndicatorSettings();
            //selectionIndicatorSettings.Color = Color.FromHex("#FAFAFA");
            //selectionIndicatorSettings.Position = SelectionIndicatorPosition.Top;
            //selectionIndicatorSettings.StrokeThickness = 0;

            //tab.TabHeight = 60;
            //tab.TabHeaderPosition = TabHeaderPosition.Bottom;
            //tab.SelectionIndicatorSettings = selectionIndicatorSettings;
            //tab.DisplayMode = TabDisplayMode.ImageWithText;
            //tab.VisibleHeaderCount = 5;
            //tab.Items = tabItems;

            //Master = new CandidateMaster();
            //Detail = new ContentPage { Content = tab };
            
            //MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            
        }
        //event click item in menu 
        //private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MenuItem;
        //    if (item != null)
        //    {
        //        _navigationService.NavigateToAsync(item.TargetType);
        //        MasterPage.ListView.SelectedItem = null;
        //        IsPresented = false;
        //    }
        //}
    }
}