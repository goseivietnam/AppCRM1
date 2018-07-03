using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class SwipeEndedCommandSfListView
    {
        public static readonly Xamarin.Forms.BindableProperty SwipeEndedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "SwipeEndedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(SwipeEndedCommandSfListView),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var listView = bindable as SfListView;

            if (listView != null)
            {
                listView.SwipeEnded -= SfListViewOnSwipeEnded;
                listView.SwipeEnded += SfListViewOnSwipeEnded;
            }
        }

        private static void SfListViewOnSwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            var listView = sender as SfListView;

            if (listView != null && listView.IsEnabled)
            {
                var command = GetSwipeEndedCommand(listView);
                if (command != null && command.CanExecute(e))
                {
                    command.Execute(e);
                }
            }
        }

        public static System.Windows.Input.ICommand GetSwipeEndedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(SwipeEndedCommandProperty);
        }

        public static void SetSwipeEndedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(SwipeEndedCommandProperty, value);
        }
    }
}
