using AppCRM.Controls;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class FocusChangedCommandBorderlessEntry
    {
        public static readonly Xamarin.Forms.BindableProperty FocusChangedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "FocusChangedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(FocusChangedCommandBorderlessEntry),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entry = bindable as BorderlessEntry;

            if (entry != null)
            {
                entry.Focused -= BorderlessEntryOnFocusChanged;
                entry.Focused += BorderlessEntryOnFocusChanged;
                entry.Unfocused -= BorderlessEntryOnFocusChanged;
                entry.Unfocused += BorderlessEntryOnFocusChanged;
            }
        }

        private static void BorderlessEntryOnFocusChanged(object sender, FocusEventArgs e)
        {
            var entry = sender as BorderlessEntry;

            if (entry != null && entry.IsEnabled)
            {
                var command = GetFocusChangedCommand(entry);
                if (command != null && command.CanExecute(e))
                {
                    command.Execute(e);
                }
            }
        }

        public static System.Windows.Input.ICommand GetFocusChangedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(FocusChangedCommandProperty);
        }

        public static void SetFocusChangedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(FocusChangedCommandProperty, value);
        }
    }
}
