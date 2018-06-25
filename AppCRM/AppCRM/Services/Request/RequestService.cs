using AppCRM.Controls;
using AppCRM.Tools;
using MimeTypes.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace AppCRM.Services.Request
{
    public interface IRequestService
    {
        Task<TResult> GetAsync1<TResult>(string queryString);
        Task<dynamic> getDataFromService(string queryString);
        Task<dynamic> getDataFromServiceAuthority(string queryString);
        Task<dynamic> postDataFromService(string url, object item);
        Task<dynamic> postDataFromServiceAuthority(string url, object item);
        Task<dynamic> UploadFileWithParameters(string url, SJFileStream stream, string fileName, List<HeaderParameters> parameters);
        Task<dynamic> UploadFile(string url, SJFileStream stream, string fileName);

    }
    public class RequestService : IRequestService
    {
        public static readonly string HOST_NAME = "http://4a5b04d0.ngrok.io/";
        public static string ACCESS_TOKEN;
        private readonly JsonSerializerSettings _serializerSettings;

        public RequestService()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

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

        public async Task<dynamic> UploadFileWithParameters(string url, SJFileStream stream, string fileName, List<HeaderParameters> parameters)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(HOST_NAME),
                Timeout = TimeSpan.FromMilliseconds(180000)
            };
            client.DefaultRequestHeaders.Add("APP_VERSION", "1.0.0");
            client.DefaultRequestHeaders.Add("TenantName", "Go2Whoa");

            //Add parameters to header
            foreach (var para in parameters)
            {
                client.DefaultRequestHeaders.Add(para.Name, para.Value);
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
            MultipartFormDataContent content = new MultipartFormDataContent();
            byte[] buffer = Tools.Utilities.ReadToEnd(stream.Stream);
            ByteArrayContent baContent = new ByteArrayContent(buffer);
            baContent.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeMap.GetMimeType(Utilities.getExtension(fileName)));
            content.Add(baContent, "File", fileName);

            //upload MultipartFormDataContent content async and store response in response var
            HttpResponseMessage response = await client.PostAsync(url, content);
            string json = response.Content.ReadAsStringAsync().Result;
            dynamic result = JsonConvert.DeserializeAnonymousType(json, new Dictionary<string, object>());

            return result;
        }

        public async Task<dynamic> UploadFile(string url, SJFileStream stream, string fileName)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(HOST_NAME),
                Timeout = TimeSpan.FromMilliseconds(180000)
            };
            client.DefaultRequestHeaders.Add("APP_VERSION", "1.0.0");
            client.DefaultRequestHeaders.Add("TenantName", "Go2Whoa");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
            MultipartFormDataContent content = new MultipartFormDataContent();
            byte[] buffer = Tools.Utilities.ReadToEnd(stream.Stream);
            ByteArrayContent baContent = new ByteArrayContent(buffer);
            baContent.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeMap.GetMimeType(Utilities.getExtension(fileName)));
            content.Add(baContent, "File", fileName);

            //upload MultipartFormDataContent content async and store response in response var
            HttpResponseMessage response = await client.PostAsync(url, content);
            string json = response.Content.ReadAsStringAsync().Result;
            dynamic result = JsonConvert.DeserializeAnonymousType(json, new Dictionary<string, object>());

            return result;
        }

        public async Task<TResult> GetAsync1<TResult>(string queryString)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(HOST_NAME),
                Timeout = TimeSpan.FromMilliseconds(180000)
            };
            client.DefaultRequestHeaders.Add("APP_VERSION", "1.0.0");
            client.DefaultRequestHeaders.Add("TenantName", "Go2Whoa");
            try
            {
                var response = await client.GetAsync(HOST_NAME + queryString);
                string json = response.Content.ReadAsStringAsync().Result;
                TResult data = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(json,_serializerSettings));
                return data;
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
    }
}

