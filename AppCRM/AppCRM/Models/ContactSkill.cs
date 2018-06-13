using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppCRM.Models
{
    public class ContactSkill
    {
        public Guid? ContactMeasurementID { get; set; }

        public Guid? ContactID { get; set; }

        public Guid? UserRoleModuleID { get; set; }

        public Guid? MeasurementID { get; set; }

        public string MeasurementName { get; set; }

        public Guid? ExperienceID { get; set; }

        public string ExperienceName { get; set; }

        public string MeasurementNameAndExperienceName
        {
            get
            {
                return string.Format("{0} - {1}", MeasurementName, ExperienceName);
            }
        }

        public ObservableCollection<PickerItem> ExperienceDDL { get; set; }
    }
}
