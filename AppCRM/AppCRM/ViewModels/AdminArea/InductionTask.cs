using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.ViewModels.AdminArea
{
    public class InductionTask
    {
    }

    public class TaskAssessment
    {
        public Guid TaskId { get; set; }
        public Guid AssessmentId { get; set; }
        public string AssessmentName { get; set; }
    }

    public class UserTaskGroup
    {
        public Guid TaskGroupID { get; set; }
        public string TaskGroupName { get; set; }
        public string Description { get; set; }
        public int TotalTask { get; set; }
        public int FinishedTask { get; set; }
        public Guid? AccountID { get; set; }
    }

    public class UserTask
    {
        public int Order { get; set; }
        public Guid TaskID { get; set; }
        public Guid TaskGroupID { get; set; }
        public string TaskName { get; set; }
        public Guid? CourseID { get; set; }
        public Guid? AccountID { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int NumberValue { get; set; }
        public string TaskTypeName { get; set; }
        public string RoleNameID { get; set; }
        public string RoleGroupID { get; set; }
        public string ContactID { get; set; }
        public bool Candidate { get; set; }
        public bool Owner { get; set; }
        public bool Manager { get; set; }
        public bool OneUpManager { get; set; }
        public DateTime? ModifieldOn { get; set; }
        public Guid ModifieldBy { get; set; }
    }

    public class UserVacancyTaskGroup
    {
        public Guid? VacancyTaskGroupID { get; set; }
        public Guid? TaskGroupID { get; set; }
        public Guid? VacancyID { get; set; }
        public Guid? StatusID { get; set; }
    }

    public class UserVacancyDocumentGroup
    {
        public Guid? VacancyDocumentGroupID { get; set; }
        public Guid? DocumentGroupID { get; set; }
        public Guid? VacancyID { get; set; }
        public Guid? StatusID { get; set; }
        public bool Owner { get; set; }
        public bool Manager { get; set; }
        public bool OneUpManager { get; set; }
        public string ResponderRoleNameID { get; set; }
        public string ResponderRoleGroupID { get; set; }
        public string ResponderAccountHolderID { get; set; }
        //public IEnumerable<BindingData> ResponderRoleNameBind { get; set; }
        //public IEnumerable<BindingData> ResponderRoleGroupBind { get; set; }
        //public IEnumerable<BindingData> ResponderAccountHolderBind { get; set; }
    }

    public class UserContactTask
    {
        public Guid ContactTaskID { get; set; }
        public Guid TaskID { get; set; }
        public Guid TaskGroupID { get; set; }
        public Guid ContactID { get; set; }
        public Guid StatusID { get; set; }
        public Guid VacancyID { get; set; }
        public Boolean Completed { get; set; }
        public DateTime? FinishDate { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public Guid AccountID { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
