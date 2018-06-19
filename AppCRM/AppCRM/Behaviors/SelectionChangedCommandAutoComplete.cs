using Syncfusion.SfAutoComplete.XForms;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class SelectionChangedCommandAutoComplete
    {
        public static readonly Xamarin.Forms.BindableProperty SelectionChangedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "SelectionChangedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(SelectionChangedCommandAutoComplete),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var autoComplete = bindable as SfAutoComplete;

            if (autoComplete != null)
            {
                autoComplete.SelectionChanged -= AutoCompleteOnSelectionChanged;
                autoComplete.SelectionChanged += AutoCompleteOnSelectionChanged;
            }
        }

        private static void AutoCompleteOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var autoComplete = sender as SfAutoComplete;

            if (autoComplete != null && autoComplete.IsEnabled)
            {
                autoComplete.SelectedItem = null;
                var command = GetSelectionChangedCommand(autoComplete);
                if (command != null && command.CanExecute(autoComplete.SelectedValue))
                {
                    command.Execute(autoComplete.SelectedValue);
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
