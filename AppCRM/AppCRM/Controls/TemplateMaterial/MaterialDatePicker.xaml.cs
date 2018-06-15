using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCRM.Controls.TemplateMaterial
{
    public partial class MaterialDatePicker : ContentView
    {
        public event EventHandler<FocusEventArgs> EntryUnfocused;

        public static BindableProperty CustomDateFormatProperty = BindableProperty.Create(nameof(CustomDateFormat), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);
        public string CustomDateFormat
        {
            get
            {
                return (string)GetValue(CustomDateFormatProperty);
            }
            set
            {
                SetValue(CustomDateFormatProperty, value);
            }
        }
        private static string _defaultDateFormat = "dd/MM/yyyy";
        public static BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.Placeholder = (string)newval;
            matEntry.HiddenLabel.Text = (string)newval;
        });

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialDatePicker), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.IsPassword = (bool)newVal;
        });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialDatePicker), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.Keyboard = (Keyboard)newVal;
        });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialDatePicker), defaultValue: Color.Accent);
        public static BindableProperty InvalidColorProperty = BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(MaterialEntry), Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntry), true, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.UpdateValidation();
        });
        public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialEntry), 14.0, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.FontSize = (double)newVal;
        });
        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialDatePicker), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.PlaceholderColor = (Color)newVal;
        });
        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialDatePicker), Color.Black, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.EntryField.TextColor = (Color)newVal;
        });
        public static BindableProperty HiddenLabelTextSizeProperty = BindableProperty.Create(nameof(HiddenLabelTextSize), typeof(double), typeof(MaterialDatePicker), 10.0, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (MaterialDatePicker)bindable;
            matEntry.HiddenLabel.FontSize = (double)newVal;
        });
        public bool IsValid
        {
            get
            {
                return (bool)GetValue(IsValidProperty);
            }
            set
            {
                SetValue(IsValidProperty, value);
            }
        }
        public Color DefaultColor
        {
            get
            {
                return (Color)GetValue(DefaultColorProperty);
            }
            set
            {
                SetValue(DefaultColorProperty, value);
            }
        }
        public Color InvalidColor
        {
            get
            {
                return (Color)GetValue(InvalidColorProperty);
            }
            set
            {
                SetValue(InvalidColorProperty, value);
            }
        }

        public DateTime? Date
        {
            get
            {
                return (DateTime?)GetValue(DateProperty);
            }
            set
            {
                SetValue(DateProperty, value);
            }
        }
        public Color AccentColor
        {
            get
            {
                return (Color)GetValue(AccentColorProperty);
            }
            set
            {
                SetValue(AccentColorProperty, value);
            }
        }
        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }
            set
            {
                SetValue(KeyboardProperty, value);
            }
        }

        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
        [Xamarin.Forms.TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }
        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }
            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }
        [Xamarin.Forms.TypeConverter(typeof(FontSizeConverter))]
        public double HiddenLabelTextSize
        {
            get
            {
                return (double)GetValue(HiddenLabelTextSizeProperty);
            }
            set
            {
                SetValue(HiddenLabelTextSizeProperty, value);
            }
        }

        public MaterialDatePicker()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            Picker.BindingContext = this;
            BottomBorder.BackgroundColor = DefaultColor;
            EntryField.Focused += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    EntryField.Unfocus();
                    Picker.Focus();
                });
            };
            Picker.Focused += async (s, a) =>
            {
                await CalculateLayoutFocused();
            };
            Picker.Unfocused += async (s, a) =>
            {
                EntryUnfocused?.Invoke(this, a);
                await CalculateLayoutUnfocused();
            };

            Picker.PropertyChanged += async (sender, args) =>
            {
                if (args.PropertyName == nameof(Picker.NullableDate))
                {
                    CustomDateFormat = CustomDateFormat ?? _defaultDateFormat;
                    var datepicker = (BorderlessDatePicker)sender;
                    EntryField.Text = datepicker.NullableDate.Value.ToString(CustomDateFormat, CultureInfo.CurrentCulture);
                    this.Date = datepicker.NullableDate;
                    await CalculateLayoutUnfocused();
                }
            };

            //UpdateValidation();
        }

        /// <summary>
        /// Calculates the layout when unfocused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        /// <returns>The layout unfocused.</returns>
        private async Task CalculateLayoutUnfocused()
        {
            if (IsValid)
            {
                HiddenLabel.TextColor = DefaultColor;
                BottomBorder.BackgroundColor = DefaultColor;
            }
            if (string.IsNullOrEmpty(EntryField.Text))
            {
                // animate both at the same time
                await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(0, 180),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y, 200, Easing.BounceIn)
                );
                EntryField.Placeholder = Placeholder;
            }
            else
            {
                HiddenLabel.IsVisible = true;
                await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
            }
        }

        /// <summary>
        /// Calculates the layout when focused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        private async Task CalculateLayoutFocused()
        {
            HiddenLabel.IsVisible = true;
            HiddenLabel.TextColor = AccentColor;
            HiddenBottomBorder.BackgroundColor = AccentColor;
            if (string.IsNullOrEmpty(EntryField.Text))
            {
                // animate both at the same time
                await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(1, 60),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y - EntryField.Height + 4, 200, Easing.BounceIn)
                );
                EntryField.Placeholder = null;
            }
            else
            {
                await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
            }
        }

        /// <summary>
        /// Updates view based on validation state
        /// </summary>
        private void UpdateValidation()
        {
            if (IsValid)
            {

                BottomBorder.BackgroundColor = DefaultColor;
                HiddenBottomBorder.BackgroundColor = AccentColor;
                if (IsFocused)
                {
                    HiddenLabel.TextColor = AccentColor;
                }
                else
                {
                    HiddenLabel.TextColor = DefaultColor;
                }
            }
            else
            {
                BottomBorder.BackgroundColor = InvalidColor;
                HiddenBottomBorder.BackgroundColor = InvalidColor;
                HiddenLabel.TextColor = InvalidColor;
            }
        }
    }
}
