using AppCRM.Services.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCRM.ViewModels.Base
{
    public abstract class ViewModelBase : BindableObject
    {
        #region property
        private bool _isBusy;
        protected readonly INavigationService NavigationService;
        #endregion 

        #region Constructor
        public ViewModelBase()
        {
            NavigationService = Locator.Instance.Resolve<INavigationService>();
        }
        #endregion

        #region Field
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region method
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        #endregion
    }
}
