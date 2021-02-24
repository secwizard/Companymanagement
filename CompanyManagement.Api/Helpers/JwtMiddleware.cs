using CompanyManagement.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using CompanyManagement.Api.Generic;

namespace CompanyManagement.Api.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public JwtMiddleware(RequestDelegate next
            , IOptions<AppSettings> appSettings
            )
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var data = GetUserInfoById(token);
                context.Items["User"] = data;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }

        private UserInfo GetUserInfoById(string bearerToken)
        {
            UserInfo userInfo = new UserInfo();
            try
            {
                string jsonString = string.Empty;
                var apihost = _appSettings.UserManagemnetAPI;
                string url = $"{apihost}/api/users/validate";
                Uri requestUri = new Uri(url);
                HttpWebRequest req = WebRequest.Create(requestUri) as HttpWebRequest;
                req.Headers["Authorization"] = "Bearer " + bearerToken;
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonString = sr.ReadToEnd();
                    }
                }
                userInfo = JsonConvert.DeserializeObject<UserInfo>(jsonString);
                if (!string.IsNullOrEmpty(userInfo.UserId.ToString()))
                {
                    return userInfo;
                }
            }
            catch (Exception ex)
            {
                log.Error("\n Error Message: " + ex.Message + " InnerException: " + ex.InnerException + "StackTrace " + ex.StackTrace.ToString());
            }
            return null;
        }
    }
}



