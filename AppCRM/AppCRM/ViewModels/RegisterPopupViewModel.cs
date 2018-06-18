using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Dialog;
using AppCRM.Utils;
using AppCRM.ViewModels.Account;
using AppCRM.ViewModels.Base;
using AppCRM.Views.Account;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels
{
    public class RegisterPopupViewModel:ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public RegisterPopupViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public ICommand CandidateRegisterCommand => new AsyncCommand(CandidateRegisterAsync);
        public ICommand EmployerRegisterCommand => new AsyncCommand(EmployerRegisterrAsync);


        private async Task CandidateRegisterAsync()
        {
            var popup = new CandidateRegisterPage();
            var viewModel = Locator.Instance.Resolve<CandidateRegisterViewModel>() as ViewModelBase;
            popup.BindingContext = viewModel;
            await PopupNavigation.Instance.PushAsync(popup);
        }


        private async Task EmployerRegisterrAsync()
        {
            var popup = new EmployerRegisterPage();
            var viewModel = Locator.Instance.Resolve<EmployerRegisterViewModel>() as ViewModelBase;
            popup.BindingContext = viewModel;
            await PopupNavigation.Instance.PushAsync(popup);
        }
       
    }
}
