using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCRM.Models
{
    public class CandidateJob
    {
        public string UserID { get; set; }
        public List<JobDetail> AppliedJobs { get; set; }
        public List<JobDetail> ReferenceCheckJobs { get; set; }
        public List<JobDetail> AssessmentJobs { get; set; }
        public List<JobDetail> ShortlistJobs { get; set; }
        public List<AssessmentDetail> NeedActionAssessments { get; set; }
        public List<AssessmentDetail> CompleteAssessments { get; set; }
    }

    public class JobDetail
    {
        public string JobDetailId { get; set; }
        public string ImageSource { get; set; }
        public string JobName { get; set; }
        public string CompanyName { get; set; }
        public string JobType { get; set; }
        public float Salary { get; set; }
        public string Location { get; set; }
        public int DayRemain { get; set; }
        public string Status { get; set; }
        public bool IsFavorite { get; set; }
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
