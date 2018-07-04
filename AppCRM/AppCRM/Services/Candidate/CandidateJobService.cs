using AppCRM.Models;
using AppCRM.Services.Request;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace AppCRM.Services.Candidate
{
    public interface ICandidateJobService
    {
        Task<dynamic> GetCandidateJobApplied();
        Task<dynamic> GetAssessment(ContactTemplateFilter filter);
        Task<dynamic> GetContactTaskByContactIDAndVacancyID(Guid? VacancyID);
        Task<dynamic> GetDocumentsAssigneedByContactIDAndVacancyID(Guid? VacancyID);
        Task<dynamic> GetVacancyDetails(Guid? VacancyID);
        Task<dynamic> WithDrawVacancy(Guid? VacancyID);
        Task<dynamic> ApplyVacancy(Guid? VacancyID);
        Task<dynamic> ShortListJob(bool? shortlisted, Guid? VacancyID);

        //Task<dynamic> GetEmployerCandidateDetail();
        //Task<dynamic> GetEmployerCandidateProfile();
        //Task<dynamic> AddEducation(ContactEducation eduction);
        //Task<dynamic> AddWorkExprience(ContactWorkExprience workExprience);
        //Task<dynamic> AddSkill(ContactSkill skill);
        //Task<dynamic> AddQualification(ContactQualification qualification);
        //Task<dynamic> AddLicence(ContactLicence licence);
        //Task<dynamic> AddDocument(Document document);
        //Task<dynamic> AddReference(ContactReference reference);
        //Task<dynamic> EditCandidateDetails(CandidateProfile profile);
        //Task<dynamic> GetCandidateExperience();
        //Task<dynamic> CandidateRegister(Register reg);
        //Task<dynamic> SaveEducationAttachment(string ContactEducationID, SJFileStream stream);
    }

    class CandidateJobService : ICandidateJobService
    {
        private readonly IRequestService _requestService;

        public CandidateJobService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<dynamic> GetCandidateJobApplied()
        {
            var result = await _requestService.getDataFromServiceAuthority("api/CandidateJob/GetCandidateJobApplied");
            return result;
        }

        public async Task<dynamic> GetAssessment(ContactTemplateFilter filter)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateJob/GetAssessment", filter);
            return result;
        }

        public async Task<dynamic> GetContactTaskByContactIDAndVacancyID(Guid? VacancyID)
        {
            object item = new Hashtable { { "VacancyID", VacancyID } };
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateJob/GetContactTaskByContactIDAndVacancyID", item);
            return result;
        }

        public async Task<dynamic> GetDocumentsAssigneedByContactIDAndVacancyID(Guid? VacancyID)
        {
            object item = new Hashtable { { "VacancyID", VacancyID } };
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateJob/GetDocumentsAssigneedByContactIDAndVacancyID", item);
            return result;
        }

        public async Task<dynamic> GetVacancyDetails(Guid? VacancyID)
        {
            var result = await _requestService.getDataFromServiceAuthority("api/CandidateJob/GetVacancyDetails?VacancyID=" + VacancyID.ToString());
            return result;
        }

        public async Task<dynamic> WithDrawVacancy(Guid? VacancyID)
        {
            object item = new Hashtable { { "VacancyID", VacancyID } };
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateJob/WithDrawVacancy", item);
            return result;
        }

        public async Task<dynamic> ApplyVacancy(Guid? VacancyID)
        {
            object item = new Hashtable { { "VacancyID", VacancyID } };
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateJob/ApplyVacancy", item);
            return result;
        }

        public async Task<dynamic> ShortListJob(bool? shortlisted, Guid? VacancyID)
        {
            object item = new Hashtable { { "VacancyID", VacancyID }, { "shortlisted", shortlisted } };
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateJob/ShortListJob", item);
            return result;
        }
    }
}
