using CompanyManagement.Api.Models;
using CompanyManagement.Api.Models.Request;
using CompanyManagement.Api.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public interface ICompanyService
    {
        Task<CompanyInfo> GetCompany(RequestBase request);
        Task<CompanyInfo> CheckCompanyUrlAndShortName(RequestCheckCompanyUrlAndShortName request);
        Task<CompanyInfo> CheckCompanyUrlFrontend(RequestCheckCompanyUrlAndShortName request);
        Task<ResponseCompanyDtlByIdFrontend> GetCompanyDtlByIdFrontend(RequestBase request);
        Task<bool> GetIsPINRequired(RequestBase request);
        Task<string> GetCompanyCurrencyCode(RequestBase request);
        Task<CompanyDetailsForSentMail> GetCompanyDetailsForSentMail(RequestBase request);
        Task<Response<CompanyInfo>> EditCompany(CompanyInfo request);
        Task<ResponseCompanyDtlByIdFrontend> GetCompanyByUrl(RequestCompanyUrl request);
        Task<List<CompanyInfo>> GetCompanyList();
        Task<CompanyMailServer> GetCompanySmtp(RequestBase request);
        Task<Response<CompanyMailServer>> EditSTMPServer(CompanyMailServer request);
        Task<List<GetCompanyTheme>> GetCompanyTheme(RequestBase request);
        Task<ResponseList<GetCompanyTheme>> EditTheme(GetCompanyTheme request);
        Task<ResponseList<GetCompanyTheme>> DeleteTheme(DeleteCompanyTheme request);
        Task<List<BranchInfo>> GetCompanyBranch(RequestBase request);
        Task<ResponseList<BranchInfo>> EditBranch(Branch request);
        Task<ResponseList<BranchInfo>> DeleteBranch(DeleteCompanyBranch request);
        Task<List<CompanySettingInfo>> GetCompanySetting(RequestCompanySetting request);
        Task<ResponseList<CompanySettingInfo>> EditCompanySetting(CompanySettingInfo request);
        Task<ResponseList<CompanySettingInfo>> DeleteCompanySetting(DeleteCompanySettings request);
        Task<List<GetCompanyTemplate>> GetCompanyTemplate(RequestBase request);
        Task<ResponseList<GetCompanyTemplate>> EditTemplate(Template request);
        Task<ResponseList<GetCompanyTemplate>> DeleteTemplate(DeleteCompanyTemplate request);
        Task<List<GetLookUpType>> GetCompanyLookUp(RequestLookUp request);

        Task<ResponseMail> SendMail(NotificationMetadata notificationMetadata, RequestSendMail requestSendMail);
    }
}
