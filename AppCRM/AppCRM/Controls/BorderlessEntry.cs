using System;
using Xamarin.Forms;

namespace AppCRM.Controls
{
    public class BorderlessEntry : Entry
    {
        public new event EventHandler Completed;

        public static readonly BindableProperty HasFocusProperty = BindableProperty.Create(nameof(HasFocus), typeof(bool), typeof(BorderlessEntry), false, propertyChanged: (entry, oldValue, newValue) =>
        {
            var borderlessEntry = (BorderlessEntry)entry;
            if ((bool)newValue && !borderlessEntry.IsFocused)
                borderlessEntry.Focus();
            else if (!(bool)newValue && borderlessEntry.IsFocused)
                borderlessEntry.Unfocus();
        });

        public bool HasFocus
        {
            get
            {
                var boundValue = (bool)GetValue(HasFocusProperty);
                if (boundValue != IsFocused)
                    SetValue(HasFocusProperty, IsFocused);
                return IsFocused;
            }
            set { SetValue(HasFocusProperty, value); }
        }

        public BorderlessEntry()
        {
            Focused += BorderlessEntry_FocusChanged;
            Unfocused += BorderlessEntry_FocusChanged;
        }

        private void BorderlessEntry_FocusChanged(object sender, FocusEventArgs e)
        {
            SetValue(HasFocusProperty, e.IsFocused);
        }
    }
}
