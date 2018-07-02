using AppCRM.Services.Request;
using AppCRM.ViewModels.AdminArea;
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

    public class ContactVacancy
    {
        public Guid ContactVacancyID { get; set; }
        public Guid? ContactID { get; set; }
        public Guid? VacancyID { get; set; }
        public DateTime AppliedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? StatusID { get; set; }
        public Guid? PipelineID { get; set; }
        public bool IsShortlisted { get; set; }
        public string StatusName { get; set; }
        public bool IsOpen { get; set; }
        public bool IsPromoted { get; set; }
        public Guid? AccountID { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string Description { get; set; }
        public string CurrencyName { get; set; }
        public string Location { get; set; }
        public string AccountName { get; set; }
        public string PoisitionName { get; set; }
        public string WorksiteName { get; set; }
        public string JobTypeName { get; set; }
        //public string ContentCoverLetter { get; set; }

        //public Guid? Signature { get; set; }

        //public string SignaturePath => Signature.HasValue ?
        //                                $"/img?id={Signature}" : null;

        //public string TransferredDate { get; set; }

        //public String ReferenceCheck { get; set; }
        //public String ReferenceCheckLabel { get; set; }
        //public String ContactTemplateReferenceIDs { get; set; }
        //public String ContactReferenceIDs { get; set; }
        //public List<ContactReference> ContactReferenceList { get; set; } = new List<ContactReference>();

        //public String Assessment { get; set; }
        //public String AssessmentLabel { get; set; }
        public int AppliedDay { get { return (int)Math.Round((DateTime.Now - this.AppliedDate).TotalDays); } }
        public string ImageSource { get => RequestService.HOST_NAME + "api/Document/GetAccountImageByContactID?id=" + AccountID.ToString(); }
        public List<JobRequire> Requires { get; set; }
        public List<UserContactTask> ToDoTasks { get; set; }
        public List<UserContactTask> CompleteTasks { get; set; }
    }

    public class Vacancy
    {
        public Guid? VacancyID { get; set; }
        public Guid? OwnerID { get; set; }
        public Guid? ManagerID { get; set; }
        public Guid? OneupManagerID { get; set; }
        public Guid? ObjectID { get; set; }
        public Guid? ContactID { get; set; }
        public string Title { get; set; }
        public Guid? SalaryRangeUnitID { get; set; }
        public string Salary { get; set; }
        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }
        public Guid? CountryID { get; set; }
        public string Country { get; set; }
        public Guid? StatusID { get; set; }
        public string Status { get; set; }
        public int PipelineOrder { get; set; }
        public bool? IsCoverLetterRequired { get; set; }
        public Guid? JobTypeID { get; set; }
        public string JobType { get; set; }
        public Guid? ClassificationID { get; set; }
        public string ClassificationName { get; set; }
        public DateTime? OpenDate { get; set; }
        public int OpenDateInt { get; set; }

        //public DateTime? AppliedDate { get; set; }        

        public Guid PipelineGroupID { get; set; }

        public DateTime? CloseDate { get; set; }

        public string Description
        {
            get; set;
        }
        public Guid? ModifiedBy { get; set; }

        public Account Account { get; set; }

        public Position Position { get; set; }

        public string WorksiteName { get; set; }

        public IEnumerable<string> ListJobRequired { get; set; }
        public string VideoLink { get; set; }
        public List<ContactDocument> ContactDocuments { get; set; }
        public bool IsPromoted { get; set; }
        public string ThirdPartyURL { get; set; }

        public bool IsClosed { get; set; }
        public bool IsShowSalary { get; set; }
        public List<JobRequire> Requires { get; set; }
        public List<ContactTemplate> Assessments { get; set; }
        public List<UserContactTask> ContactTasksTodo { get; set; }
        public List<UserContactTask> ContactTasksComplete { get; set; }
        public int RemainingDay { get { return (int)Math.Round((this.CloseDate.Value - DateTime.Now).TotalDays); } }
        public string ImageSource { get => RequestService.HOST_NAME + "api/Document/GetAccountImageByContactID?id=" + Account.AccountID.ToString(); }
    }

    public class ContactTemplateFilter
    {
        public Guid? ContactId { get; set; }
        public string AssessmentKeyWord { get; set; }

        //public string WorksiteId { get; set; }

        public string TemplateId { get; set; }

        public string PositionId { get; set; }

        public int PageSize { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;
        public bool IsOpen { get; set; }

        public string Mode { get; set; } = "GET";
        public Guid? Assister { get; set; }

        public string AssessmentType { get; set; }
    }

    public class ContactTemplate
    {
        public Guid ContactTemplateID { get; set; }
        public Guid? CandidateContactTemplateID { get; set; }//this use when current instance hold employer, if candidate will be null or equals ContactTemplateID

        public Guid? ContactID { get; set; }

        public Guid? TemplateID { get; set; }

        public Guid? StatusID { get; set; }

        public Guid? ContactReferenceID { get; set; }

        public Guid? WhoIsFor { get; set; }

        public Guid? VacancyID { get; set; }

        public string TemplateName { get; set; }

        public string CandidateName { get; set; }

        public string StateName { get; set; }

        public string CompleteStatus { get; set; }

        public Guid? CreatedTemplateId { get; set; }

        //public string Author
        //{
        //    get
        //    {
        //        if (this.CreatedTemplateId.HasValue)
        //        {
        //            Contact AuthorInfo = new Contact().GetContactDetails(this.CreatedTemplateId);
        //            return AuthorInfo.FullName;
        //        }
        //        return String.Empty;
        //    }
        //}

        public DateTime? CreatedDate { get; set; }

        //public AssessmentType TypeOfAssessment { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? AssignDate { get; set; }

        public bool IsCompleted { get; set; }

        public int NumberQuestion { get; set; }

        public List<Contact> ContactsForAssessmentView { get; set; }

        public Contact Contact { get; set; }

        public AccountJobs Vacancy { get; set; }

        public Status Status { get; set; }

        public string Assessor { get; set; }

        public string ContactTemplateViewDetailsUrl { get; set; }

        public bool? IsFinished { get; set; }

        public string Roles { get; set; }
    }

    public class JobRequire
    {
        public Guid? VacancyMeasurementID { get; set; }
        public Guid? VacancyID { get; set; }
        public Guid? MeasurementID { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementType { get; set; }
    }

    public class AssessmentStatus
    {
        public const string NEED_ACTION = "NeedAction";
        public const string COMPLETE = "Complete";
    }

    public class Position
    {
        public Guid PositionID { get; set; }

        public string Name { get; set; }

        public Guid? TenantID { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? Modifiedon { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class ContactJobs
    {
        #region Variable Declaration
        public Guid? ContactID { get; set; }

        public Contact Contact { get; set; }

        public AccountJobs Job { get; set; }

        public DateTime? AppliedDate { get; set; }

        public Guid? AccountID { get; set; }

        public Guid? StatusID { get; set; }

        public Guid? TenantID { get; set; }

        public Guid? TenantLogo { get; set; }
        public String TenantName { get; set; }

        public Guid? ClassificationID { get; set; }
        public Guid? SalaryRangeUnitID { get; set; }
        public Guid? JobTypeID { get; set; }
        public String JobTypeName { get; set; }
        public String Status { get; set; }
        public Guid? EmployerID { get; set; }
        public int DateRange { get; set; }
        public Guid? VacancyID { get; set; }
        public String Company { get; set; }
        public String Title { get; set; }
        public String Salary { get; set; }
        public String Country { get; set; }
        public String Applied { get; set; }

        public Guid? ObjectID { get; set; }
        public String KeyWord { get; set; }
        public String Location { get; set; }
        public String JobType { get; set; }
        public String Description { get; set; }
        public DateTime? OpenDate { get; set; }
        public Guid? AccountLogo { get; set; }
        public String CurrencyName { get; set; }
        public string ProfileImageLink { get; set; }
        public bool? IsCoverLetterRequired { get; set; }
        public string ContentCoverLetter { get; set; }
        public Guid? Signature { get; set; }
        public Guid? FavouriteCandidateID { get; set; }
        public bool? IsPromoted { get; set; }
        public bool? IsOwner { get; set; }
        public bool? AreRefereesRequired { get; set; }
        public string ThirdPartyURL { get; set; }
        public string WorksiteName { get; set; }
        public int OpenDurationDay { get { return (int)Math.Round((DateTime.Now - this.OpenDate.Value).TotalDays); } }
        public string ImageSource { get => RequestService.HOST_NAME + "api/Document/GetAccountImageByContactID?id=" + AccountID.ToString(); }
        #endregion
    }
}
