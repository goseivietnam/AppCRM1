using System;
using Autofac;
using AppCRM.Services.Navigation;
using AppCRM.Services.Authentication;
using AppCRM.Services.Request;
using AppCRM.Services.Dialog;
using AppCRM.ViewModels.Account;
using AppCRM.Services.CandidateDetail;

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
            //ViewMode
            _containerBuilder.RegisterType<LoginViewModel>();
            _containerBuilder.RegisterType<MainViewModel>();
            _containerBuilder.RegisterType<CandidateRegisterViewModel>();
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
