using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class ContactQualification
    {
        public Guid? ContactMeasurementID { get; set; }
        public Guid? ContactID { get; set; }
        public Guid? UserRoleModuleID { get; set; }

        public Guid? MeasurementID { get; set; }
        public string MeasurementName { get; set; }

        public DateTime? From { get; set; }

        public string DateFromString { get; set; }

        //[GreaterThan("From", PassOnNull = true)]
        public DateTime? To { get; set; }

        public string DateToString { get; set; }

        public string Institute { get; set; }

        public Guid? DocumentID { get; set; }

        public string DocumentName { get; set; }

        public string DocumentThumbnailUri { get; set; }

        public string DocumentDownloadUri { get; set; }

        public string MeasurementNameAndInstitute
        {
            get
            {
                return string.Format("{0} - {1}", MeasurementName, Institute);
            }
        }

        public string FromAndTo
        {
            get
            {
                return string.Format("{0} - {1}", DateFromString, DateToString);
            }
        }
    }
}
