using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class InterestedRole
    {
        public Guid? ID { get; set; }
        public Guid? TenantID { get; set; }
        public string Name { get; set; }
    }
}
