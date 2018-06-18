using Xamarin.Forms;

namespace AppCRM.Behaviors
{
    public sealed class ItemTappedCommandListView
    {
        public static readonly Xamarin.Forms.BindableProperty ItemTappedCommandProperty =
            Xamarin.Forms.BindableProperty.CreateAttached(
                "ItemTappedCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(ItemTappedCommandListView),
                default(System.Windows.Input.ICommand),
                Xamarin.Forms.BindingMode.OneWay,
                null,
                PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var listView = bindable as ListView;

            if (listView != null)
            {
                listView.ItemTapped -= ListViewOnItemTapped;
                listView.ItemTapped += ListViewOnItemTapped;
            }
        }

        private static void ListViewOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var list = sender as ListView;

            if (list != null && list.IsEnabled && !list.IsRefreshing)
            {
                list.SelectedItem = null;
                var command = GetItemTappedCommand(list);
                if (command != null && command.CanExecute(e.Item))
                {
                    command.Execute(e.Item);
                }
            }
        }

        public static System.Windows.Input.ICommand GetItemTappedCommand(BindableObject bindableObject)
        {
            return (System.Windows.Input.ICommand)bindableObject.GetValue(ItemTappedCommandProperty);
        }

        public static void SetItemTappedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(ItemTappedCommandProperty, value);
        }
    }
}
