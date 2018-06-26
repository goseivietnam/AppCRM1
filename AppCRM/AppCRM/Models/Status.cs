using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class Status
    {
        public Guid StatusID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int? PipelineOrder { get; set; }

        public Guid? TenantID { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid? PipelineID { get; set; }

        public string EmailTemplateID { get; set; }

        //public bool? AssessmentRequired { get; set; }

        //public bool? ReferenceCheckRequired { get; set; }

        //public bool? InductionRequired { get; set; }
        //public bool? DocumentRequired { get; set; }

        //public bool? IsEmployeeAgreement { get; set; }
        //public bool? RestrictMovement { get; set; }
        //public bool AutoCreateEmployee { get; set; }
        //public Guid? VacancyStatusAssessRefID { get; set; }
        //public Guid? VacancyStatusRestrictMovementID { get; set; }
        //public bool Owner { get; set; }
        //public bool Manager { get; set; }
        //public bool OneUpManager { get; set; }
        //public bool ReviewerOwner { get; set; }
        //public bool ReviewerManager { get; set; }
        //public bool ReviewerOneUpManager { get; set; }
        //public bool ResponderOwner { get; set; }
        //public bool ResponderManager { get; set; }
        //public bool ResponderOneUpManager { get; set; }
        //public bool AssisterOwner { get; set; }
        //public bool AssisterManager { get; set; }
        //public bool AssisterOneUpManager { get; set; }
        //public Guid? TemplateID { get; set; }
        //public Guid? VacancyID { get; set; }
        //public Guid? TaskGroupID { get; set; }
        //public Guid? DocumentGroupID { get; set; }
        //public Guid? VacancyTaskGroupID { get; set; }
        //public Guid? VacancyDocumentGroupID { get; set; }
        //public string ResponderRoleNameID { get; set; }
        //public string ResponderRoleGroupID { get; set; }
        //public string ResponderAccountHolderID { get; set; }
        //public string CompanyRoleID { get; set; }
        //public string RoleGroupID { get; set; }
        //public string AccountHolderID { get; set; }
        //public string TemplateTypeName { get; set; }

        //public bool JobClosed { get; set; }

        //public Guid? PipelineGroupID { get; set; }
        //public Guid? Assister { get; set; }
        //public Guid? DefaultPipelineManagementID { get; set; }
    }
}
