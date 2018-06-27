using AppCRM.Controls;
using AppCRM.Models;
using AppCRM.Services.Request;
using AppCRM.Tools;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Task<dynamic> AddDocument(SJFileStream stream, string fileName);
        Task<dynamic> AddReference(ContactReference reference);
        Task<dynamic> EditCandidateDetails(Contact profile);
        Task<dynamic> GetCandidateExperience();
        Task<dynamic> CandidateRegister(Register reg);
        Task<dynamic> SaveEducationAttachment(string ContactEducationID, SJFileStream stream);
        Task<dynamic> SaveWorkExperienceAttachment(string WorkExperienceID, SJFileStream stream);
        Task<dynamic> SaveContactQualificationAttachment(string ContactQualificationID, SJFileStream stream);
        Task<dynamic> SaveContactLicenceAttachment(string ContactLicenceID, SJFileStream stream);
        Task<dynamic> AddEditContactCoverImage(SJFileStream stream);
        Task<dynamic> AddEditContactAvatarImage(SJFileStream stream);
        Task<dynamic> UploadResume(SJFileStream stream);
        Task<IEnumerable<PickerItem>> GetInterestedLocationsDDL();
        Task<IEnumerable<PickerItem>> GetInterestedRolesDDL();
    }
    public class CandidateDetailsService : ICandidateDetailsService
    {
        private readonly IRequestService _requestService;

        public CandidateDetailsService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<dynamic> GetEmployerCandidateDetail()
        {
            var result = await _requestService.getDataFromServiceAuthority("api/CandidateDetails/GetEmployerCandidateDetail?ContactID=" + App.ContactID.ToString());
            return result;
        }

        public async Task<dynamic> GetEmployerCandidateProfile()
        {
            var result = await _requestService.getDataFromServiceAuthority("api/CandidateDetails/GetEmployerCandidateProfile?ContactID=" + App.ContactID.ToString());
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

        public async Task<dynamic> AddDocument(SJFileStream stream, string fileName)
        {
            var result = await _requestService.UploadFile("api/CandidateDetails/AddEditContactDocuments", stream, fileName);
            return result;
        }

        public async Task<dynamic> AddReference(ContactReference reference)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateDetails/AddEditContactReferences", reference);
            return result;
        }

        public async Task<dynamic> EditCandidateDetails(Contact profile)
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

        public async Task<dynamic> SaveWorkExperienceAttachment(string ContactWorkExperienceID, SJFileStream stream)
        {
            List<HeaderParameters> parameters = new List<HeaderParameters>();
            parameters.Add(new HeaderParameters("ContactWorkExperienceID", ContactWorkExperienceID));
            var result = await _requestService.UploadFileWithParameters("api/CandidateDetails/SaveWorkExperienceAttachment", stream, stream.FileName, parameters);
            return result;
        }

        public async Task<dynamic> SaveContactQualificationAttachment(string ContactQualificationID, SJFileStream stream)
        {
            List<HeaderParameters> parameters = new List<HeaderParameters>();
            parameters.Add(new HeaderParameters("ContactQualificationID", ContactQualificationID));
            var result = await _requestService.UploadFileWithParameters("api/CandidateDetails/SaveContactQualificationAttachment", stream, stream.FileName, parameters);
            return result;
        }

        public async Task<dynamic> SaveContactLicenceAttachment(string ContactLicenceID, SJFileStream stream)
        {
            List<HeaderParameters> parameters = new List<HeaderParameters>();
            parameters.Add(new HeaderParameters("ContactLicenceID", ContactLicenceID));
            var result = await _requestService.UploadFileWithParameters("api/CandidateDetails/SaveContactLicenceAttachment", stream, stream.FileName, parameters);
            return result;
        }

        public async Task<dynamic> AddEditContactCoverImage(SJFileStream stream)
        {
            var result = await _requestService.UploadFile("api/CandidateDetails/AddEditContactCoverImage", stream, stream.FileName);
            return result;
        }

        public async Task<dynamic> AddEditContactAvatarImage(SJFileStream stream)
        {
            var result = await _requestService.UploadFile("api/CandidateDetails/AddEditContactAvatarImage", stream, stream.FileName);
            return result;
        }

        public async Task<dynamic> UploadResume(SJFileStream stream)
        {
            var result = await _requestService.UploadFile("api/Document/UploadResume", stream, stream.FileName);
            return result;
        }

        public async Task<IEnumerable<PickerItem>> GetInterestedLocationsDDL()
        {
            return await _requestService.GetAsync<IEnumerable<PickerItem>>("api/DDL/GetInterestedLocationsDDL?Filter");
        }

        public async Task<IEnumerable<PickerItem>> GetInterestedRolesDDL()
        {
            return await _requestService.GetAsync<IEnumerable<PickerItem>>("api/DDL/GetInterestedRolesDDL?Filter");

        }
    }
}
