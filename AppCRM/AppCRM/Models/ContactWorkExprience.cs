using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class ContactWorkExprience
    {
        public Guid? ContactID { get; set; }

        public Guid? ContactWorkExperienceID { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime? From { get; set; }

        public string TimeFromString { get; set; }

        public DateTime? To { get; set; }

        public string TimeToString { get; set; }

        public Guid? UserRoleModuleID { get; set; }

        public string Experience { get; set; }

        public Guid? DocumentID { get; set; }

        public string DocumentName { get; set; }

        public bool IsWorkCurrent { get; set; }

        public string TitleJobAndLocation
        {
            get
            {
                return string.Format("{0} - {1}", Title, Location);
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
