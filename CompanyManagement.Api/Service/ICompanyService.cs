using CompanyManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public interface ICompanyService
    {
        Task<CompanyInfo> GetCompany(RequestBase request);
        Task<List<CompanyInfo>> GetCompanyList();
        Task<CompanyMailServer> GetCompanySmtp(RequestBase request);
        Task<List<GetCompanyTheme>> GetCompanyTheme(RequestBase request);
        Task<ResponseList<GetCompanyTemplate>> EditTheme(Template request);
        Task<List<BranchInfo>> GetCompanyBranch(RequestBase request);
        Task<List<CompanySettingInfo>> GetCompanySetting(RequestCompanySetting request);
        Task<Response<CompanyInfo>> EditCompany(CompanyInfo request);
        Task<Response<CompanyMailServer>> EditSTMPServer(CompanyMailServer request);
        Task<ResponseList<CompanySettingInfo>> EditCompanySetting(CompanySettingInfo request);
        Task<List<GetCompanyTemplate>> GetCompanyTemplate(RequestBase request);
        Task<ResponseList<GetCompanyTemplate>> EditTemplate(Template request);
        Task<List<GetLookUpType>> GetCompanyLookUp(RequestLookUp request);
    }
}
