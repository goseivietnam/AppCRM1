using System;
using System.Collections.Generic;

namespace AppCRM.Models
{
    public class CandidateJob
    {
        public string UserID { get; set; }
        public List<ContactVacancy> ContactVacancies { get; set; }
        public List<AssessmentDetail> NeedActionAssessments { get; set; }
        public List<AssessmentDetail> CompleteAssessments { get; set; }
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
        public string ImageSource { get; set; }
        public List<JobRequire> Requires { get; set; }
        public List<JobTask> ToDoTasks { get; set; }
        public List<JobTask> CompleteTasks { get; set; }
        public List<JobAttachment> Attachments { get; set; }
    }

    public class AssessmentDetail
    {
        public string AssessmentDetailId { get; set; }
        public string ImageSource { get; set; }
        public string AssessmentName { get; set; }
        public string JobName { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
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
