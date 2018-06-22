using AppCRM.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class AccountJobs
    {
        #region Variable Declaration
        public Contact Contact { get; set; }

        public Guid? ContactID { get; set; }
        public Guid? AccountID { get; set; }
        public String AccountName { get; set; }
        public Guid? JobTypeID { get; set; }
        public String JobTypeName { get; set; }
        public String Status { get; set; }
        public Guid? StatusID { get; set; }
        public string StatusName { get; set; }
        public Guid? EmployerID { get; set; }
        public int? DateRange { get; set; }
        public Guid? VacancyID { get; set; }
        public String Company { get; set; }
        public String Title { get; set; }
        public String Salary { get; set; }
        public String Country { get; set; }
        public bool Applied { get; set; }
        public Guid? ObjectID { get; set; }
        public string ImageSource { get => RequestService.HOST_NAME + "api/Document/GetAccountImageByContactID?id=" + ObjectID.ToString(); }

        public String KeyWord { get; set; }
        public String Location { get; set; }
        public string CityName { get; set; }
        public String Skill { get; set; }
        public String Qualification { get; set; }
        public String Ticket { get; set; }
        public String JobType { get; set; }
        public String CurrencyName { get; set; }

        //public Guid? AccountLogoID { get; set; }

        //public bool? IsCoverLetterRequired { get; set; }

        //public int AttachmentsCount { get; set; }

        //public int CommentsCount { get; set; }

        //public int EventsCount { get; set; }

        //public int ColouredNode { get; set; }

        //public Guid OwnerID { get; set; }

        //public Guid ManagerID { get; set; }

        //public Guid OneupManagerID { get; set; }

        public String Worksite { get; set; }
        public String Position { get; set; }

        public DateTime? OpenDate { get; set; }


        public DateTime? CloseDate { get; set; }
 
        //public int Quantity { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ClassificationName { get; set; }

        public Guid? ClassificationID { get; set; }
        public Guid UserRoleModuleID { get; set; }
        public Guid? SalaryRangeUnitID { get; set; }
        public String SalaryRangeUnitName { get; set; }
        public String Mode { get; set; }
        public Decimal? MinSalary { get; set; }
        public Decimal? MaxSalary { get; set; }
        public Guid? CurrencyID { get; set; }

        public string Currency { get; set; }

        //public String DocumentName { get; set; }
        //public String DocumentType { get; set; }
        //public byte[] Document { get; set; }

        //public Guid? AccountLogo { get; set; }
        //public string AccountLogoUrl { get; set; }

        //public String Candidate { get; set; }
        //public bool Interested { get; set; }

        public int PageSize { get; set; } = 22;

        public int CurrentPage { get; set; } = 1;
        //public string DetailsUrl { get; set; }
        //public string AddToShortListUrl { get; set; }
        //public string ReturnAfterEdit { get; set; }

        public Guid? PositionID { get; set; }
        public Guid? WorksiteID { get; set; }
        public Guid? CityID { get; set; }
        public Guid CountryID { get; set; }
        public Guid? StateID { get; set; }

        //public string AccountProfileUrl { get; set; }

        public string ShortDescription { get; set; }


        public string PositionName { get; set; }

        public string WorksiteName { get; set; }
        public Guid? ProfileImage { get; set; }

        //public int ShortlistedCount { get; set; }
        //public string VideoLink { get; set; }

        //public string TransferredDate { get; set; }

        //public Guid? ModifiedBy { get; set; }

        //public DateTime? ModifiedOn { get; set; }
        //[DisplayName("Modified On")]
        //public string ModifiedOnString
        //{
        //    get
        //    {
        //        return this.ModifiedOn.HasValue ? this.ModifiedOn.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : null;
        //    }
        //    set
        //    {
        //        this.ModifiedOn = Utilities.GetDateTimeFromString(value);
        //    }
        //}
        public Guid? TenantId { get; set; }

        //public Guid? PipelineGroupID { get; set; }

        //public bool? IsRequiredQualification { get; set; }

        //public bool? IsRequiredExperience { get; set; }

        //public bool? IsRequiredCitizenPerResident { get; set; }

        //public bool IsClosed { get; set; }
        //public string ThirdPartyURL { get; set; }

        //public List<VacancyAssessment> AssessmentList { get; set; }

        #endregion
    }
}
