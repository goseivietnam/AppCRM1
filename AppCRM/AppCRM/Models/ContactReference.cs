using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class ContactReference
    {
        public Guid? ContactID { get; set; }

        public Guid? UserRoleModuleID { get; set; }

        public Guid? ContactReferenceID { get; set; }

        public string EmployerName { get; set; }

        public string Position { get; set; }

        public string ReferenceName { get; set; }

        public string Relationship { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool Contacted { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PostalCode { get; set; }

        public bool CanViewDetails { get; set; }
    }
}
