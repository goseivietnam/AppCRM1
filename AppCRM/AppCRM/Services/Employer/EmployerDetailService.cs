using AppCRM.Models;
using AppCRM.Services.Request;
using System;
using System.Threading.Tasks;

namespace AppCRM.Services.Employer
{
    public interface IEmployerDetailService
    {
        Task<dynamic> EmployerRegister(Register reg);
    }
    public class EmployerDetailService : IEmployerDetailService
    {
        private readonly IRequestService _requestService;

        public EmployerDetailService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<dynamic> EmployerRegister(Register reg)
        {
            var result = await _requestService.postDataFromService("api/Account/EmployerRegister", reg);
            return result;
        }
    }
}
