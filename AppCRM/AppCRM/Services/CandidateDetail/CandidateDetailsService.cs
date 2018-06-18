using AppCRM.Controls;
using AppCRM.Models;
using AppCRM.Services.Request;
using AppCRM.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCRM.Services.CandidateDetail
{
    public interface ICandidateDetailsService
    {
        Task<dynamic> GetEmployerCandidateDetail();
        Task<dynamic> GetEmployerCandidateProfile();
        Task<dynamic> AddEducation(ContactEducation eduction);
        Task<dynamic> AddWorkExprience(ContactWorkExprience workExprience);
        Task<dynamic> AddSkill(ContactSkill skill);
        Task<dynamic> AddQualification(ContactQualification qualification);
        Task<dynamic> AddLicence(ContactLicence licence);
        Task<dynamic> AddDocument(Document document);
        Task<dynamic> AddReference(ContactReference reference);
        Task<dynamic> EditCandidateDetails(CandidateProfile profile);
        Task<dynamic> GetCandidateExperience();
        Task<dynamic> CandidateRegister(Register reg);
        Task<dynamic> SaveEducationAttachment(string ContactEducationID, SJFileStream stream);
    }
    public class CandidateDetailsService:ICandidateDetailsService
    {
        private readonly IRequestService _requestService;
        public CandidateDetailsService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<dynamic> GetEmployerCandidateDetail()
        {
            var result = await _requestService.getDataFromServiceAuthority("api/CandidateDetails/GetEmployerCandidateDetail?ContactID=" + App.UserID.ToString());
            return result;
        }

        public async Task<dynamic> GetEmployerCandidateProfile()
        {
            var result = await _requestService.getDataFromServiceAuthority("api/CandidateDetails/GetEmployerCandidateProfile?ContactID=" + App.UserID.ToString());
            return result;
        }

        public async Task<dynamic> AddEducation(ContactEducation eduction)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactEducations", eduction);
            return result;
        }

        public async Task<dynamic> AddWorkExprience(ContactWorkExprience workExprience)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactWorkExperiences", workExprience);
            return result;
        }

        public async Task<dynamic> AddSkill(ContactSkill skill)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactSkills", skill);
            return result;
        }

        public async Task<dynamic> AddQualification(ContactQualification qualification)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactQualifications", qualification);
            return result;
        }

        public async Task<dynamic> AddLicence(ContactLicence licence)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactLicences", licence);
            return result;
        }

        public async Task<dynamic> AddDocument(Document document)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactDocuments", document);
            return result;
        }

        public async Task<dynamic> AddReference(ContactReference reference)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactReferences", reference);
            return result;
        }

        public async Task<dynamic> EditCandidateDetails(CandidateProfile profile)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactDetails", profile);
            return result;
        }

        public async Task<dynamic> GetCandidateExperience()
        {
            var result = await _requestService.getDataFromServiceAuthority("api/CandidateDetails/GetCandidateExperienceDDL");
            return result;
        }

        public async Task<dynamic> CandidateRegister(Register reg)
        {
            var result = await _requestService.postDataFromService("api/Account/CandidateRegister", reg);
            return result;
        }

        public async Task<dynamic> SaveEducationAttachment(string ContactEducationID, SJFileStream stream)
        {
            List<HeaderParameters> parameters = new List<HeaderParameters>();
            parameters.Add(new HeaderParameters("ContactEducationID", ContactEducationID));
            var result = await _requestService.UploadFileWithParameters("api/CandidateDetails/SaveEducationAttachment", stream, stream.FileName, parameters);
            return result;
        }
    }
}
