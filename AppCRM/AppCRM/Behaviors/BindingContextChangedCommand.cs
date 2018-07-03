using Syncfusion.ListView.XForms;
using System;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class BindingContextChangedCommand
    {
        public static readonly Xamarin.Forms.BindableProperty BindingContextChangedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "BindingContextChangedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(SwipeEndedCommandSfListView),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var element = bindable;

            if (element != null)
            {
                element.BindingContextChanged -= ElementBindingContextChanged;
                element.BindingContextChanged += ElementBindingContextChanged;
            }
        }

        private static void ElementBindingContextChanged(object sender, EventArgs e)
        {
            var element = sender as BindableObject;

            if (element != null)
            {
                var command = GetBindingContextChangedCommand(element);
                if (command != null && command.CanExecute(sender))
                {
                    command.Execute(sender);
                }
            }
        }

        public static System.Windows.Input.ICommand GetBindingContextChangedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(BindingContextChangedCommandProperty);
        }

        public static void SetBindingContextChangedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(BindingContextChangedCommandProperty, value);
        }
    }
}
