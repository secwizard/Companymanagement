using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.UI.Services
{
    public interface IServiceAPI
    {
        string ProcessRequest(string baseURL, string endPoint, string authorizationToken, bool bearerToken = true);
        string ProcessRequest(string endPoint, string authorizationToken, bool bearerToken = true);
        string ProcessRequest(string endPoint);

        string ProcessPostRequest(string url, string postdata, string authorizationToken, bool bearerToken=true);
        string ProcessPostRequest(string url, string postdata);
    }
}
