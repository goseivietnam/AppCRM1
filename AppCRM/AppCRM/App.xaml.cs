using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Navigation;
using AppCRM.ViewModels;
using AppCRM.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AppCRM
{
	public partial class App : Application
	{
        public static string UserID { get; set; }
        static App()
        {
            BuildDependencies();
        }
        public App ()
		{
			InitializeComponent();
            InitNavigation();
        }

        private Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            return navigationService.NavigateToAsync<LoginViewModel>();
        }
        public static void BuildDependencies()
        {
            Locator.Instance.Build();
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
