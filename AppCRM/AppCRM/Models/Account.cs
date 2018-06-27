using System;

namespace AppCRM.Models
{
    public class Account
    {
        #region Variable Declaration

        public Guid AccountID { get; set; }
        public Guid ContactID { get; set; }

        public String AccountName { get; set; }

        public string Name { get; set; }

        public String Owner { get; set; }
        public String Address { get; set; }

        public String Address1 { get; set; }

        public Guid? StateID { get; set; }
        public String StateName { get; set; }

        public Guid? CityID { get; set; }

        public Guid? CountryID { get; set; }
        public String CountryCode { get; set; }
        //[Phone]
        //[StringLength(15)]
        public string Phone { get; set; }

        public string Email { get; set; }

        public Guid TenantID { get; set; }
        public Guid AddedBy { get; set; }
        public Guid UserRoleModuleID { get; set; }
        public Guid? FavouriteEmployerID { get; set; }

        public String Mode { get; set; }
        public int ContactCounter { get; set; }
        public int OpportunitiesCouter { get; set; }

        public int ActionCounter { get; set; }

        public float Value { get; set; }
        public int LostOpportunitiesCounter { get; set; }

        public string RoleName { get; set; }

        public string AboutUs
        {
            get; set;
        }

        public Guid? AccountLogo { get; set; }
        public bool AccountLogoIsCircle { get; set; }
        public Guid? CoverImage { get; set; }
        public String NoofEmployees { get; set; }
        public String ESTYear { get; set; }
        public Guid AccountTypeID { get; set; }
        public String AccountType { get; set; }
        public String FullName { get; set; }
        public String Worksite { get; set; }
        public String Position { get; set; }
        public String Country { get; set; }

        ///// <summary>
        ///// Total number of open vacancy
        ///// </summary>
        //public int VacancyNumber { get; set; }

        public string ClassificationIds { get; set; }

        public string Classification { get; set; }
        public string Quickfilter { get; set; }

        public int PageSize { get; set; } = 22;

        public int CurrentPage { get; set; } = 1;

        public string DetailsUrl { get; set; }

        public Guid PrimaryContactId { get; set; }

        public Guid? ClassificationID { get; set; }

        public string IndustryName { get; set; }
        public string WebSite { get; set; }
        public string VideoLink { get; set; }

        public string TimeZone { get; set; }

        public string CityName { get; set; }

        public string PostalCode { get; set; }

        public Guid? BusinessTypeID { get; set; }
        #endregion
    }
}
