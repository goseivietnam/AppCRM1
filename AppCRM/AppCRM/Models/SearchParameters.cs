using System;

namespace AppCRM.Models
{
   public class SearchParameters
    {
        public string Keyword { get; set; }
        public string Location { get; set; }
        public Guid? JobTypeId { get; set; }
        public string JobTypeIds { get; set; }
        public string SalaryEstimateIds { get; set; }
        public string CategoryIds { get; set; }
        public string LocationIds { get; set; }
        public string PositionIds { get; set; }
        public string SkillsIds { get; set; }
        public string QualificationsIds { get; set; }
        public string TicketLicensesIds { get; set; }
        public bool? SaveSearchCriterias { get; set; }
        public bool? IsExperience { get; set; }
        public bool? IsQualification { get; set; }
        public bool? IsWorkRights { get; set; }
        public string SalaryMin { get; set; }
        public string SalaryMax { get; set; }
        public int JobTotal { get; set; }
        public int CurrentPage { get; set; }
    }

    public class EmployerSearchFilter
    {
        public string KeySearch1 { get; set; }
        public string KeySearch2 { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
