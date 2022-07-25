using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public class ServiceAPI : IServiceAPI
    {
        public async Task<string> ProcessPostRequest(string endPoint, string postdata, string authorizationToken, bool bearerToken)
        {
            string url = endPoint;
            using var client = new WebClient();
            Uri uri = new Uri(url);
            client.Headers.Add("Content-Type:application/json");
            client.Headers.Add("Accept:application/json");
            client.Headers["Authorization"] = (bearerToken) ? "Bearer " + authorizationToken : authorizationToken;
            return await client.UploadStringTaskAsync(uri, "POST", postdata);
        }
        public async Task<string> ProcessPostRequest(string endPoint, string postdata)
        {
            string url = endPoint;
            using var client = new WebClient();
            Uri uri = new Uri(url);
            client.Headers.Add("Content-Type:application/json");
            client.Headers.Add("Accept:application/json");
            return await client.UploadStringTaskAsync(uri, "POST", postdata);
        }
    }
}
