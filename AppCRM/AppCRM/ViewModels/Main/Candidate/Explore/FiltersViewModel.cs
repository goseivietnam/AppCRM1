using AppCRM.Utils;
using AppCRM.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCRM.ViewModels.Main.Candidate.Explore
{
    public class FiltersViewModel:ViewModelBase
    {
        public ICommand BtnBackCommand => new AsyncCommand(BtnBackCommandAsync);

        private async Task BtnBackCommandAsync()
        {
                await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
