using AppCRM.Services.Request;
using AppCRM.Tools;
using System;
using System.Collections.Generic;

namespace AppCRM.Models
{
    public class CandidateJob
    {
        public string UserID { get; set; }
        public List<ContactVacancy> ContactVacancies { get; set; }
        public List<ContactTemplate> NeedActionAssessments { get; set; }
        public List<ContactTemplate> CompleteAssessments { get; set; }
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
        public List<JobTask> ToDoTasks { get; set; }
        public List<JobTask> CompleteTasks { get; set; }
        public List<JobAttachment> Attachments { get; set; }
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
        //public DateTime? AppliedDate { get; set; }        

        public Guid PipelineGroupID { get; set; }

        public DateTime? CloseDate { get; set; }

        public string Description
        {
            get; set;
        }
        public Guid? ModifiedBy { get; set; }

        //public Account Account { get; set; }

        //public Position Position { get; set; }

        public string WorksiteName { get; set; }

        public IEnumerable<string> ListJobRequired { get; set; }
        public string VideoLink { get; set; }
        public IEnumerable<Document> Documents { get; set; }
        public bool IsPromoted { get; set; }
        public string ThirdPartyURL { get; set; }

        public bool IsClosed { get; set; }
        public bool IsShowSalary { get; set; }
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
        public string JobRequireId { get; set; }
        public string RequireName { get; set; }
    }

    public class JobTask
    {
        public string JobTaskId { get; set; }
        public string TaskName { get; set; }
        public string CreatedBy { get; set; }
        public bool IsComplete { get; set; }
    }

    public class JobAttachment
    {
        public string JobAttachmentId { get; set; }
        public string AttachmentName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class JobStatus
    {
        public const string APPLIED = "Applied";
        public const string REFERENCE_CHECK = "ReferenceCheck";
        public const string ASSESSMENT = "Assessment";
        public const string SHORTLIST = "Shortlist";
    }

    public class AssessmentStatus
    {
        public const string NEED_ACTION = "NeedAction";
        public const string COMPLETE = "Complete";
    }
}
