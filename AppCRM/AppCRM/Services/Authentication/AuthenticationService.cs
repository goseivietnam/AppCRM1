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
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(RequestService.HOST_NAME),
                Timeout = TimeSpan.FromMilliseconds(180000)
            };
            client.DefaultRequestHeaders.Add("APP_VERSION", "1.0.0");
            client.DefaultRequestHeaders.Add("TenantName", "Go2Whoa");
            var item = new { UserName = username, Password = password };
            var jsonRequest = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            System.Net.WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            HttpResponseMessage response = await client.PostAsync("api/Account/Login", content);
            string json = response.Content.ReadAsStringAsync().Result;
            dynamic result = JsonConvert.DeserializeAnonymousType(json, new Dictionary<string, object>());
            return result;
        }
    }
}
