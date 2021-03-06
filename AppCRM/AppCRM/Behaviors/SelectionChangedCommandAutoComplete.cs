﻿using AppCRM.Extensions;
using AppCRM.Models;
using Syncfusion.SfAutoComplete.XForms;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
                if (autoComplete.MultiSelectMode != MultiSelectMode.None && !autoComplete.IsSelectedItemsVisibleInDropDown)
                {
                    if(autoComplete.SelectedItem.ToString() == "")
                    {
                        autoComplete.SelectedItem = new Collection<LookupItem>().Cast<object>().ToObservableCollection();
                    }
                    else
                    {
                        foreach(var item in autoComplete.SelectedItem as Collection<object>)
                        {
                            var lookupItem = item as LookupItem;
                            if((autoComplete.SelectedItem as Collection<object>).Count(r => (r as LookupItem).Id == lookupItem.Id) > 1)
                            {
                                (autoComplete.SelectedItem as Collection<object>).Remove(item);
                            }
                        }
                    }
                }
                var command = GetSelectionChangedCommand(autoComplete);
                if (command != null && command.CanExecute(autoComplete.SelectedItem))
                {
                    command.Execute(autoComplete.SelectedItem);
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
