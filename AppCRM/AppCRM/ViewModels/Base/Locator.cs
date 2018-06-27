using System;
using Autofac;
using AppCRM.Services.Navigation;
using AppCRM.Services.Authentication;
using AppCRM.Services.Request;
using AppCRM.Services.Dialog;
using AppCRM.ViewModels.Account;
using AppCRM.Services.CandidateDetail;
using AppCRM.ViewModels.Main.Candidate;
using AppCRM.Services.Employer;
using AppCRM.ViewModels.Main.Candidate.Profile;
using AppCRM.Services.Candidate;
using AppCRM.ViewModels.Main.Candidate.Job;

namespace AppCRM.ViewModels.Base
{
    public class Locator
    {
        #region property
        private IContainer _container;
        private ContainerBuilder _containerBuilder;
        private static readonly Locator _instance = new Locator();
        public static Locator Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region Constructor
        public Locator()
        {
            _containerBuilder = new ContainerBuilder();

            //Service
            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            _containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            _containerBuilder.RegisterType<RequestService>().As<IRequestService >();
            _containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            _containerBuilder.RegisterType<CandidateDetailsService>().As<ICandidateDetailsService>();
            _containerBuilder.RegisterType<CandidateJobService>().As<ICandidateJobService>();
            _containerBuilder.RegisterType<EmployerDetailService>().As<IEmployerDetailService>();
            //ViewModel
            _containerBuilder.RegisterType<LoginViewModel>();
            _containerBuilder.RegisterType<MainViewModel>();
            _containerBuilder.RegisterType<CandidateRegisterViewModel>();
            _containerBuilder.RegisterType<EmployerRegisterViewModel>();
            _containerBuilder.RegisterType<CandidateProfileViewModel>();
            _containerBuilder.RegisterType<CandidateJobViewModel>();
            _containerBuilder.RegisterType<CandidateMainViewModel>();
            _containerBuilder.RegisterType<RegisterPopupViewModel>();
            _containerBuilder.RegisterType<AddEducationViewModel>();
            _containerBuilder.RegisterType<AddDocumentViewModel>();
            _containerBuilder.RegisterType<AddLicenceViewModel>();
            _containerBuilder.RegisterType<AddQualificationViewModel>();
            _containerBuilder.RegisterType<AddReferenceViewModel>();
            _containerBuilder.RegisterType<AddSkillPageViewModel>();
            _containerBuilder.RegisterType<AddWorkExprienceViewModel>();
            _containerBuilder.RegisterType<EditProfileViewModel>();
            _containerBuilder.RegisterType<AccountSettingViewModel>();
            _containerBuilder.RegisterType<JobDetailViewModel>();
        }
        #endregion

        #region Method
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void Register<T>() where T : class
        {
            _containerBuilder.RegisterType<T>();
        }

        public void Build()
        {
            _container = _containerBuilder.Build();
        }
        #endregion

    }
}
