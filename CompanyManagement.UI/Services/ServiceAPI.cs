using CompanyManagement.UI.Helpers;
using log4net;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Services
{
    public class ServiceAPI : IServiceAPI
    {
        private readonly AppSettings _appSettings;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ServiceAPI(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string ProcessRequest(string baseURL, string endPoint, string authorizationToken, bool bearerToken)
        {
            string jsonString = string.Empty;
            string url = baseURL + endPoint;
            try
            {
                Uri requestUri = new Uri(url);
                HttpWebRequest req = WebRequest.Create(requestUri) as HttpWebRequest;
                req.Headers["Authorization"] = (bearerToken) ? "Bearer " + authorizationToken : authorizationToken;
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonString = sr.ReadToEnd();
                    }
                }
                return jsonString;
            }
            catch (Exception ex)
            {
                log.Error("ProcessGetRequest Url : " + url + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                throw ex;
            }
        }
        public string ProcessRequest(string endPoint, string authorizationToken, bool bearerToken)
        {
            string jsonString = string.Empty;
            string url = _appSettings.ApiHost + "/" + endPoint;
            try
            {
                Uri requestUri = new Uri(url);
                HttpWebRequest req = WebRequest.Create(requestUri) as HttpWebRequest;
                req.Headers["Authorization"] = (bearerToken) ? "Bearer " + authorizationToken : authorizationToken;
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonString = sr.ReadToEnd();
                    }
                }
                return jsonString;
            }
            catch (Exception ex)
            {
                log.Error("ProcessGetRequest Url : " + url + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                throw ex;
            }
        }
        public string ProcessRequest(string endPoint)
        {
            string jsonString = string.Empty;
            string url = _appSettings.ApiHost + "/" + endPoint;
            try
            {
                Uri requestUri = new Uri(url);
                HttpWebRequest req = WebRequest.Create(requestUri) as HttpWebRequest;
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonString = sr.ReadToEnd();
                    }
                }
                return jsonString;
            }
            catch (Exception ex)
            {
                log.Error("ProcessGetRequest Url : " + url + " Error " + ex.Message + "StackTrace " + ex.StackTrace.ToString());
                throw ex;
            }
        }
        public string ProcessPostRequest(string endPoint, string postdata, string authorizationToken, bool bearerToken)
        {
            string url = _appSettings.ApiHost + "/" + endPoint;
            using (var client = new WebClient())
            {
                Uri uri = new Uri(url);
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                client.Headers["Authorization"] = (bearerToken) ? "Bearer " + authorizationToken : authorizationToken;
                return client.UploadString(uri, "POST", postdata);
            }
        }
        public string ProcessPostRequest(string endPoint, string postdata)
        {
            string url = _appSettings.ApiHost + "/" + endPoint;
            using (var client = new WebClient())
            {
                Uri uri = new Uri(url);
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                return client.UploadString(uri, "POST", postdata);
            }
        }

        #region ======== Login ==========
        public async Task<string> LoginUser(string input)
        {
            string url = $"{_appSettings.UserManagementAPI}/users/authenticate";
            string response = await APIRequest(url, "POST", input);
            return response;
        }
        #endregion

        #region ======== Company ==========
        public async Task<string> CompanyDtl(string postdata, string authorizationToken, bool bearerToken)
        {
            string url = $"{_appSettings.ApiHost}/company/GetCompany";
            using var client = new WebClient();
            Uri uri = new Uri(url);
            client.Headers.Add("Content-Type:application/json");
            client.Headers.Add("Accept:application/json");
            client.Headers["Authorization"] = (bearerToken) ? "Bearer " + authorizationToken : authorizationToken;
            return await client.UploadStringTaskAsync(uri, "POST", postdata);
        }
        public async Task<string> CompanyList(string authorizationToken, bool bearerToken)
        {
            string url = $"{_appSettings.ApiHost}/company/GetCompanyList";
            using var client = new WebClient();
            Uri uri = new Uri(url);
            client.Headers.Add("Content-Type:application/json");
            client.Headers.Add("Accept:application/json");
            client.Headers["Authorization"] = (bearerToken) ? "Bearer " + authorizationToken : authorizationToken;
            return await client.UploadStringTaskAsync(uri, "POST", "");
        }
        public async Task<string> AddCompany(string postdata, string authorizationToken, bool bearerToken)
        {
            string url = $"{_appSettings.ApiHost}/OnBoard/AddCompany";
            using var client = new WebClient();
            Uri uri = new Uri(url);
            client.Headers.Add("Content-Type:application/json");
            client.Headers.Add("Accept:application/json");
            client.Headers["Authorization"] = (bearerToken) ? "Bearer " + authorizationToken : authorizationToken;
            return await client.UploadStringTaskAsync(uri, "POST", postdata);
        }
        public async Task<string> EditCompany(string postdata, string authorizationToken, bool bearerToken)
        {
            string url = $"{_appSettings.ApiHost}/company/EditCompany";
            using var client = new WebClient();
            Uri uri = new Uri(url);
            client.Headers.Add("Content-Type:application/json");
            client.Headers.Add("Accept:application/json");
            client.Headers["Authorization"] = (bearerToken) ? "Bearer " + authorizationToken : authorizationToken;
            return await client.UploadStringTaskAsync(uri, "POST", postdata);
        }
        #endregion
        #region Generic
        public async Task<string> APIRequest(string url, string method, string postdata, string token = "")
        {
            using var client = new WebClient();
            Uri uri = new Uri(url);
            client.Headers.Add("Content-Type:application/json");
            client.Headers.Add("Accept:application/json");
            client.Headers.Add("token", token);

            return await client.UploadStringTaskAsync(uri, method?.ToUpper(), postdata);
        }

        #endregion Generic


    }
}
