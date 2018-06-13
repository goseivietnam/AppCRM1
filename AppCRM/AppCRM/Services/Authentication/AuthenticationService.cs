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
    }
}
