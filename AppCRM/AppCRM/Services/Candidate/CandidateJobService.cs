using AppCRM.Controls;
using AppCRM.Models;
using AppCRM.Services.CandidateDetail;
using AppCRM.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCRM.Services.Candidate
{
    public interface ICandidateJobService
    {
        Task<dynamic> GetCandidateJobApplied();
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
    }
}
