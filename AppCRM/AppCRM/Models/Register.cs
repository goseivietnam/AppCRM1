using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class Register
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string[] JobInterest { get; set; }
        public string[] JobLocation { get; set; }
        public string AccountName { get; set; }
        public string Industry { get; set; }
        public Register()
        {

        }
    }
}
