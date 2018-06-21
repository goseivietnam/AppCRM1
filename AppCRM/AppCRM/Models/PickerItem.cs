using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class PickerItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
