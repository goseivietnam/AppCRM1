using AppCRM.Models;
using AppCRM.Services.Request;
using System;
using System.Threading.Tasks;

namespace AppCRM.Services.Employer
{
    public interface IEmployerJobService
    {
        Task<dynamic> GetEmployerList(EmployerSearchFilter filter);
        Task<dynamic> GetEmployerDetails(Guid? AccountID);
        Task<dynamic> GetRelatedJobs(AccountJobs AJ);
    }
    public class EmployerJobService : IEmployerJobService
    {
        private readonly IRequestService _requestService;

        public EmployerJobService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<dynamic> GetEmployerList(EmployerSearchFilter filter)
        {
            var result = await _requestService.postDataFromService("api/EmployerJob/GetEmployerList", filter);
            return result;
        }

        public async Task<dynamic> GetEmployerDetails(Guid? AccountID)
        {
            var result = await _requestService.getDataFromServiceAuthority("api/EmployerJob/GetEmployerDetails?AccountID=" + AccountID.ToString());
            return result;
        }

        public async Task<dynamic> GetRelatedJobs(AccountJobs AJ)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/EmployerJob/GetRelatedJobs", AJ);
            return result;
        }
    }
}
