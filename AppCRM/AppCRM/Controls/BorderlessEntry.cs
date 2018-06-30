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

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(BorderlessEntry), ReturnType.Done, BindingMode.OneWay);

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
        public ReturnType ReturnType
        {
            get { return (ReturnType)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
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

        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
        }
    }

    public enum ReturnType
    {
        Go,
        Next,
        Done,
        Send,
        Search
    }
}
