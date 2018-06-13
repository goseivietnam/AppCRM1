using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class ContactEducation
    {
        public Guid? ContactID { get; set; }

        public Guid? ContactEducationID { get; set; }

        public string University { get; set; }

        public string DegreeType { get; set; }

        public string Classification { get; set; }

        public string City { get; set; }

        public string SubClassification { get; set; }

        public DateTime? From { get; set; }

        public string TimeFromString { get; set; }

        public DateTime? To { get; set; }

        public string TimeToString { get; set; }

        public bool StillStudying { get; set; }

        public string ClassificationAndCity
        {
            get
            {
                return string.Format("{0} - {1}", Classification, City);
            }
        }

        public string FromAndTo
        {
            get
            {
                return string.Format("{0} - {1}", TimeFromString, TimeToString);
            }
        }
    }
}
