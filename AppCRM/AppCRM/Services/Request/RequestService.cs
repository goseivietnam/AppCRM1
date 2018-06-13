using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppCRM.Services.Request
{
    public interface IRequestService
    {
        Task<dynamic> getDataFromService(string queryString);
        Task<dynamic> getDataFromServiceAuthority(string queryString);
        Task<dynamic> postDataFromService(string url, object item);
        Task<dynamic> postDataFromServiceAuthority(string url, object item);

    }
    public class RequestService : IRequestService
    {
        public static readonly string HOST_NAME = "http://50.62.135.124:8033/";
        public static string ACCESS_TOKEN;

        public async Task<dynamic> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(HOST_NAME),
                Timeout = TimeSpan.FromMilliseconds(180000)
            };
            client.DefaultRequestHeaders.Add("APP_VERSION", "1.0.0");
            client.DefaultRequestHeaders.Add("TenantName", "Go2Whoa");

            var response = await client.GetAsync(HOST_NAME + queryString);

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }

        public async Task<dynamic> getDataFromServiceAuthority(string queryString)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(HOST_NAME),
                Timeout = TimeSpan.FromMilliseconds(180000)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("APP_VERSION", "1.0.0");
            client.DefaultRequestHeaders.Add("TenantName", "Go2Whoa");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

            var response = await client.GetAsync(HOST_NAME + queryString);

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }

        public async Task<dynamic> postDataFromService(string url, object item)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(HOST_NAME),
                Timeout = TimeSpan.FromMilliseconds(180000)
            };
            client.DefaultRequestHeaders.Add("APP_VERSION", "1.0.0");
            client.DefaultRequestHeaders.Add("TenantName", "Go2Whoa");

            var jsonRequest = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            System.Net.WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            HttpResponseMessage response = await client.PostAsync(url, content);
            string json = response.Content.ReadAsStringAsync().Result;
            dynamic result = JsonConvert.DeserializeAnonymousType(json, new Dictionary<string, object>());

            return result;
        }

        public async Task<dynamic> postDataFromServiceAuthority(string url, object item)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(HOST_NAME),
                Timeout = TimeSpan.FromMilliseconds(180000)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("APP_VERSION", "1.0.0");
            client.DefaultRequestHeaders.Add("TenantName", "Go2Whoa");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            var jsonRequest = JsonConvert.SerializeObject(item, microsoftDateFormatSettings);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            System.Net.WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            HttpResponseMessage response = await client.PostAsync(url, content);
            string json = response.Content.ReadAsStringAsync().Result;
            dynamic result = JsonConvert.DeserializeAnonymousType(json, new Dictionary<string, object>());

            return result;
        }
    }
}

