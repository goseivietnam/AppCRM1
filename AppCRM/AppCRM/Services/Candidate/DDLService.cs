using AppCRM.Models;
using AppCRM.Services.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCRM.Services.Candidate
{
    public interface IDDLService
    {
        Task<IEnumerable<PickerItem>> GetJobTypeDDL();
        Task<IEnumerable<PickerItem>> GetClassificationDDL();
        Task<IEnumerable<PickerItem>> GetLocationDDL(string Filter);
        Task<IEnumerable<PickerItem>> GetPositionDDL(string Filter);
        Task<IEnumerable<PickerItem>> GetSkillsDDL(string Filter);
        Task<IEnumerable<PickerItem>> GetQualificationDDL();
        Task<IEnumerable<PickerItem>> GetLicenceDDL();
        Task<InitFilter> GetInitDataFilter();
    }

    class DDLService : IDDLService
    {
        private readonly IRequestService _requestService;

        public DDLService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<IEnumerable<PickerItem>> GetJobTypeDDL()
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<PickerItem>>("api/DDL/GetJobTypeDDL");
            return result;
        }
        public async Task<IEnumerable<PickerItem>> GetClassificationDDL()
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<PickerItem>>("api/DDL/GetClassificationDDL");
            return result;
        }
        public async Task<IEnumerable<PickerItem>> GetLocationDDL(string Filter)
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<PickerItem>>("api/DDL/GetLocationDDL?Filter=" + Filter);
            return result;
        }
        public async Task<IEnumerable<PickerItem>> GetPositionDDL(string Filter)
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<PickerItem>>("api/DDL/GetPositionDDL?Filter=" + Filter);
            return result;
        }
        public async Task<IEnumerable<PickerItem>> GetSkillsDDL(string Filter)
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<PickerItem>>("api/DDL/GetSkillsDDL?Filter=" + Filter);
            return result;
        }
        public async Task<IEnumerable<PickerItem>> GetQualificationDDL()
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<PickerItem>>("api/DDL/GetQualificationDDL");
            return result;
        }
        public async Task<IEnumerable<PickerItem>> GetLicenceDDL()
        {
            var result = await _requestService.GetDDLAsyncAuthority<IEnumerable<PickerItem>>("api/DDL/GetLicenceDDL");
            return result;
        }

        public async Task<InitFilter> GetInitDataFilter()
        {
            var result = await _requestService.GetDDLAsyncAuthority<InitFilter>("api/CandidateJob/GetDLLFilterSearch");
            return result;
        }
    }
}
