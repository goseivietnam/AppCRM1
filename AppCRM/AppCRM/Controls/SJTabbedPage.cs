using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCRM.Controls
{
    public class SJTabbedPage : TabbedPage
    {
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(SJTabbedPage), 0, propertyChanged: (page, oldValue, newValue) =>
        {
            var tabbedPage = (SJTabbedPage)page;
            if(tabbedPage != null && tabbedPage.Children != null && tabbedPage.Children.Count > (int)newValue)
            {
                tabbedPage.CurrentPage = tabbedPage.Children[(int)newValue];
            }
        });

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }
    }
}
