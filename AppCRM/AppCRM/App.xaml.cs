﻿using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Navigation;
using AppCRM.ViewModels;
using AppCRM.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppCRM
{
    public partial class App : Application
    {
        public static string ContactID { get; set; }
        public static string UserName { get; set; } = "thuleduy01@gmail.com";
        public static string PassWord { get; set; } = "12345";

        static App()
        {
            BuildDependencies();
        }
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzgyNEAzMTM2MmUzMjJlMzBhdThPQ0s1em5NandpNVNHeHNBbDRpdDJxVFVmR0Q0Y3ErdEhDZ0FVYlNZPQ==");
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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
