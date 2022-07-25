using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public interface IServiceAPI
    {
        Task<string> ProcessPostRequest(string url, string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> ProcessPostRequest(string url, string postdata);
    }
}
