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

        #region ========== Login ==========
        Task<string> LoginUser(string input);
        #endregion
        #region ========== Company ==========
        Task<string> CompanyDtl(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> CompanyList(string authorizationToken, bool bearerToken = true);
        Task<string> AddCompany(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> EditCompany(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> GetMailDetails(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> EditSTMPServer(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> GetCompanySettingsDetails(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> EditCompanySetting(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> GetTemplateDetails(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> EditTemplate(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> GetThemeDetails(string postdata, string authorizationToken, bool bearerToken = true);
        Task<string> EditTheme(string postdata, string authorizationToken, bool bearerToken = true);
        #endregion
    }
    
}
