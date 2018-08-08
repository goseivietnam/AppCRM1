using AppCRM.Controls;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class CurrentPageChangedCommandTabbedPage
    {
        public static readonly Xamarin.Forms.BindableProperty CurrentPageChangedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "CurrentPageChangedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(CurrentPageChangedCommandTabbedPage),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SJTabbedPage sJTabbedPage)
            {
                sJTabbedPage.CurrentPageChanged -= TabbedPageOnCurrentPageChanged;
                sJTabbedPage.CurrentPageChanged += TabbedPageOnCurrentPageChanged;
            }
        }

        private static void TabbedPageOnCurrentPageChanged(object sender, EventArgs e)
        {
            if (sender is SJTabbedPage sJTabbedPage && sJTabbedPage.IsEnabled)
            {
                var currentIndex = MultiPage<Page>.GetIndex(sJTabbedPage.CurrentPage);
                sJTabbedPage.SelectedIndex = currentIndex;
                var command = GetCurrentPageChangedCommand(sJTabbedPage);
                if (command != null && command.CanExecute(currentIndex))
                {
                    command.Execute(currentIndex);
                }
            }
        }

        public static System.Windows.Input.ICommand GetCurrentPageChangedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(CurrentPageChangedCommandProperty);
        }

        public static void SetCurrentPageChangedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(CurrentPageChangedCommandProperty, value);
        }
    }
}
