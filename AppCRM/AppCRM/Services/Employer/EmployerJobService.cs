using AppCRM.Models;
using AppCRM.Services.Request;
using System;
using System.Threading.Tasks;

namespace AppCRM.Services.Employer
{
    public interface IEmployerJobService
    {
        Task<dynamic> GetEmployerList(EmployerSearchFilter filter);
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
    }
}
