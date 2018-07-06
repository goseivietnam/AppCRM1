using AppCRM.Models;
using AppCRM.Services.Request;
using AppCRM.ViewModels.Main.Candidate.Explore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCRM.Services.Candidate
{
    public interface IDDLService
    {
        Task<IEnumerable<LookupItem>> GetJobTypeDDL();
        Task<IEnumerable<LookupItem>> GetClassificationDDL();
        Task<IEnumerable<LookupItem>> GetLocationDDL(string Filter);
        Task<IEnumerable<LookupItem>> GetPositionDDL(string Filter);
        Task<IEnumerable<LookupItem>> GetSkillsDDL(string Filter);
        Task<IEnumerable<LookupItem>> GetQualificationDDL();
        Task<IEnumerable<LookupItem>> GetLicenceDDL();
    }

    class DDLService : IDDLService
    {
        private readonly IRequestService _requestService;

        public DDLService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<IEnumerable<LookupItem>> GetJobTypeDDL()
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<LookupItem>>("api/DDL/GetJobTypeDDL");
            return result;
        }
        public async Task<IEnumerable<LookupItem>> GetClassificationDDL()
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<LookupItem>>("api/DDL/GetClassificationDDL");
            return result;
        }
        public async Task<IEnumerable<LookupItem>> GetLocationDDL(string Filter)
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<LookupItem>>("api/DDL/GetLocationDDL?Filter=" + Filter);
            return result;
        }
        public async Task<IEnumerable<LookupItem>> GetPositionDDL(string Filter)
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<LookupItem>>("api/DDL/GetPositionDDL?Filter=" + Filter);
            return result;
        }
        public async Task<IEnumerable<LookupItem>> GetSkillsDDL(string Filter)
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<LookupItem>>("api/DDL/GetSkillsDDL?Filter=" + Filter);
            return result;
        }
        public async Task<IEnumerable<LookupItem>> GetQualificationDDL()
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<LookupItem>>("api/DDL/GetQualificationDDL");
            return result;
        }
        public async Task<IEnumerable<LookupItem>> GetLicenceDDL()
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<LookupItem>>("api/DDL/GetLicenceDDL");
            return result;
        }
    }
}
