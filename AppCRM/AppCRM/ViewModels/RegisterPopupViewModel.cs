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
    public class RegisterPopupViewModel
    {
        private readonly IDialogService _dialogService;

        public RegisterPopupViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public ICommand CandidateRegisterCommand => new AsyncCommand(CandidateRegisterAsync);

        private async Task CandidateRegisterAsync()
        {
            var popup = new CandidateRegisterPage();
            var viewModel = new CandidateRegisterViewModel(_dialogService);
            popup.BindingContext = viewModel;
            await PopupNavigation.Instance.PushAsync(popup);
        }

        public ICommand EmployerRegisterCommand => new AsyncCommand(EmployerRegisterrAsync);

        private async Task EmployerRegisterrAsync()
        {
            await _dialogService.PopupMessage("Bạn đã nhấn nút đăng kí2", "#52CD9F", "#FFFFFF");
        }
    }
}
