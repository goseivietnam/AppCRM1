using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class LoadedCommandSfListView
    {
        public static readonly Xamarin.Forms.BindableProperty LoadedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "LoadedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(LoadedCommandSfListView),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var listView = bindable as SfListView;

            if (listView != null)
            {
                listView.Loaded -= SfListViewOnLoaded;
                listView.Loaded += SfListViewOnLoaded;
            }
        }

        private static void SfListViewOnLoaded(object sender, ListViewLoadedEventArgs e)
        {
            var listView = sender as SfListView;

            if (listView != null && listView.IsEnabled)
            {
                var command = GetLoadedCommand(listView);
                if (command != null && command.CanExecute(sender))
                {
                    command.Execute(sender);
                }
            }
        }

        public static System.Windows.Input.ICommand GetLoadedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(LoadedCommandProperty);
        }

        public static void SetLoadedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(LoadedCommandProperty, value);
        }
    }
}
