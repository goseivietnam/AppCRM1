using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public string LogoUrl { get; set; }
    }
}
