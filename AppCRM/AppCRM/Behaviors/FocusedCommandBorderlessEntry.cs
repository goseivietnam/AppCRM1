using AppCRM.Controls;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class FocusedCommandBorderlessEntry
    {
        public static readonly Xamarin.Forms.BindableProperty FocusedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "FocusedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(FocusedCommandBorderlessEntry),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entry = bindable as BorderlessEntry;

            if (entry != null)
            {
                entry.Focused -= BorderlessEntryOnFocused;
                entry.Focused += BorderlessEntryOnFocused;
            }
        }

        private static void BorderlessEntryOnFocused(object sender, FocusEventArgs e)
        {
            var entry = sender as BorderlessEntry;

            if (entry != null && entry.IsEnabled)
            {
                var command = GetFocusedCommand(entry);
                if (command != null && command.CanExecute(e.IsFocused))
                {
                    command.Execute(e.IsFocused);
                }
            }
        }

        public static System.Windows.Input.ICommand GetFocusedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(FocusedCommandProperty);
        }

        public static void SetFocusedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(FocusedCommandProperty, value);
        }
    }
}
