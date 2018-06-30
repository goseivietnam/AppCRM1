using AppCRM.Controls;
using System;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class CompletedCommandBorderlessEntry
    {
        public static readonly Xamarin.Forms.BindableProperty CompletedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "CompletedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(CompletedCommandBorderlessEntry),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entry = bindable as BorderlessEntry;

            if (entry != null)
            {
                entry.Completed -= BorderlessEntryOnCompleted;
                entry.Completed += BorderlessEntryOnCompleted;
            }
        }

        private static void BorderlessEntryOnCompleted(object sender, EventArgs e)
        {
            var entry = sender as BorderlessEntry;

            if (entry != null && entry.IsEnabled)
            {
                var command = GetCompletedCommand(entry);
                if (command != null && command.CanExecute(entry.Text))
                {
                    command.Execute(entry.Text);
                }
            }
        }

        public static System.Windows.Input.ICommand GetCompletedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(CompletedCommandProperty);
        }

        public static void SetCompletedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(CompletedCommandProperty, value);
        }
    }
}
