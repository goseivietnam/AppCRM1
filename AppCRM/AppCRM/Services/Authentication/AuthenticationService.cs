using AppCRM.Models;
using AppCRM.Services.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppCRM.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<dynamic> LoginAsync(string username, string password);
        Task<dynamic> CandidateRegister(Register reg);
        Task<dynamic> EmployerRegister(Register reg);
        Task<dynamic> ChangePassword(Contact profile);
        Task<dynamic> Logout();
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestService _requestService;

        public AuthenticationService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<dynamic> LoginAsync(string username, string password)
        {
            var item = new { UserName = username, Password = password };
            var result = await _requestService.postDataFromService("api/Account/Login", item);
            return result;
        }

        public async Task<dynamic> CandidateRegister(Register reg)
        {
            var result = await _requestService.postDataFromService("api/Account/CandidateRegister", reg);
            return result;
        }

        public async Task<dynamic> EmployerRegister(Register reg)
        {
            var result = await _requestService.postDataFromService("api/Account/EmployerRegister", reg);
            return result;
        }

        public async Task<dynamic> ChangePassword(Contact profile)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/Account/ChangePassword", profile);
            return result;
        }

        public async Task<dynamic> Logout()
        {
            var result = await _requestService.postDataFromServiceAuthority("api/Account/Logout", null);
            return result;
        }
    }
}
