using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class PickerItem
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
