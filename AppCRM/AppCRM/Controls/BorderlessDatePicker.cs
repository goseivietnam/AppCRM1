using AppCRM.Controls.TemplateMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCRM.Controls
{
    public class BorderlessDatePicker : DatePicker
    {
        private string _format = null;
        public static BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set
            {
                SetValue(NullableDateProperty, value);
                UpdateDate();
            }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                if (_format != null)
                    Format = _format;
                Date = NullableDate.Value;
            }
            else
            {
                _format = Format;
                Format = "dd/MM/yyyy";
            }
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Date")
            {
                NullableDate = Date;
            }
        }
    }
}
