using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class SelectionChangedCommandTabView
    {
        public static readonly Xamarin.Forms.BindableProperty SelectionChangedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "SelectionChangedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(SelectionChangedCommandTabView),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entry = bindable as SfTabView;

            if (entry != null)
            {
                entry.SelectionChanged -= TabViewOnSelectionChanged;
                entry.SelectionChanged += TabViewOnSelectionChanged;
            }
        }

        private static void TabViewOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabView = sender as SfTabView;

            if (tabView != null && tabView.IsEnabled)
            {
                var command = GetSelectionChangedCommand(tabView);
                if (command != null && command.CanExecute(e.Index))
                {
                    command.Execute(e.Index);
                }
            }
        }

        public static System.Windows.Input.ICommand GetSelectionChangedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(SelectionChangedCommandProperty);
        }

        public static void SetSelectionChangedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(SelectionChangedCommandProperty, value);
        }
    }
}
